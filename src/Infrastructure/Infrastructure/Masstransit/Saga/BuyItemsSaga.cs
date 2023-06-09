﻿using MassTransit;
using Microsoft.Extensions.Logging;

namespace Ark.Infrastructure.Masstransit.Saga;

public class BuyItemsRequest
{
    public Guid OrderId { get; set; }
}

public class BuyItemsResponse
{
    public Guid OrderId { get; set; }
    public string ErrorMessage { get; set; }
}

public sealed class BuyItemsSaga : MassTransitStateMachine<BuyItemsSagaState>
{
    private readonly ILogger<BuyItemsSaga> _logger;

    public BuyItemsSaga(ILogger<BuyItemsSaga> logger)
    {
        _logger = logger;
        //Указываем куда будем записывать текущее состояние саги (Pending,Faulted)
        InstanceState(x => x.CurrentState);
        //Указываем что слушаем событие OrderId у которого равен нашему CorrelationId у саги
        //Либо если нет саги с таким CorrelationId то создаем его с ним.
        Event(() => BuyItems, x => x.CorrelateById(y => y.Message.OrderId));
        //Указываем какие запросы будем делать из саги
        Request(() => GetMoney);
        Request(() => GetItems);
        //Указываем как будем реагировать на сообщения в стартовом состоянии
        Initially(
            When(BuyItems)
                .Then(x =>
                {
                    //Сохраняем идентификатор запроса и его адрес при старте саги чтобы потом на него ответить
                    if (!x.TryGetPayload(out SagaConsumeContext<BuyItemsSagaState, BuyItemsRequest>? payload) ||
                        payload is null)
                        throw new Exception("Unable to retrieve required payload for callback data.");
                    x.Saga.RequestId = payload.RequestId;
                    x.Saga.ResponseAddress = payload.ResponseAddress;
                })
                //Совершаем запрос к микросевису MoneyMicroservice
                .Request(GetMoney, x => x.Init<IGetMoneyRequest>(new {x.Message.OrderId}))
                //Переводим сагу в состояние GetMoney.Pending
                .TransitionTo(GetMoney!.Pending)
        );

        //Описываем то как наша сага будет реагировать на сообщения находясь в 
        //состоянии GetMoney.Pending
        During(GetMoney.Pending,
            //Когда приходи сообщение что запрос прошел успешно делаем новый запрос
            //теперь уже в микросервис ItemsMicroservice
            When(GetMoney.Completed)
                .Request(GetItems, x => x.Init<IGetItemsRequest>(new {x.Message.OrderId}))
                .TransitionTo(GetItems!.Pending),
            //При ошибке отвечаем тому, кто инициировал запрос сообщением с текстом ошибки
            When(GetMoney.Faulted)
                .ThenAsync(async context =>
                {
                    //Тут можно сделать какие-то компенсирующие действия. 
                    //Например, вернуть деньги куда-то на счет.
                    await RespondFromSaga(context,
                        "Faulted On Get Money " + string.Join("; ", context.Message.Exceptions.Select(x => x.Message)));
                })
                .TransitionTo(Failed),
            //При таймауте отвечаем с сообщением что произошел таймаут
            When(GetMoney.TimeoutExpired)
                .ThenAsync(async context =>
                {
                    await RespondFromSaga(context, "Timeout Expired On Get Money");
                })
                .TransitionTo(Failed)
        );

        During(GetItems.Pending,
            //При успешном ответе от микросервиса предметов 
            //отвечаем без ошибки и переводим сагу в финальное состояние.
            When(GetItems.Completed)
                .ThenAsync(async context =>
                {
                    await RespondFromSaga(context, null);
                })
                .Finalize(),
            When(GetItems.Faulted)
                .ThenAsync(async context =>
                {
                    //Тут можно сделать какие-то компенсирующие действия. 
                    //Например, вернуть деньги куда-то на счет.
                    await RespondFromSaga(context,
                        "Faulted On Get Items " + string.Join("; ", context.Data.Exceptions.Select(x => x.Message)));
                })
                .TransitionTo(Failed),
            When(GetItems.TimeoutExpired)
                .ThenAsync(async context =>
                {
                    await RespondFromSaga(context, "Timeout Expired On Get Items");
                })
                .TransitionTo(Failed)
        );
    }

    //Запрос на получение денег
    public Request<BuyItemsSagaState, IGetMoneyRequest, IGetMoneyResponse> GetMoney { get; set; }

    //Запрос на получение предметов
    public Request<BuyItemsSagaState, IGetItemsRequest, IGetItemsResponse> GetItems { get; set; }

    //Событие стартующее нашу сагу.
    public Event<BuyItemsRequest> BuyItems { get; set; }

    //Одно из наших кастомных состояний в которое может перейти сага
    public State Failed { get; set; }

    //Метод для ответного сообщения
    //Тут нужно явно использовать ResponseAddress и RequestId 
    //сохраненные ранее чтобы ответить ровно тому, кто сделал запрос
    private static async Task RespondFromSaga<T>(BehaviorContext<BuyItemsSagaState, T> context, string error)
        where T : class
    {
        var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
        await endpoint.Send(new BuyItemsResponse
        {
            OrderId = context.Saga.CorrelationId,
            ErrorMessage = error,
        }, r => r.RequestId = context.Saga.RequestId, context.CancellationToken);
    }
}

//Рекомендуется для контрактов использовать интерфейсы чтобы не было возможности 
//в них прикрутить какую-то логику.
//Хотя уже с новой версией C# где можно реализации и интерфейсам прикреплять 
//подход больше не актуален
public interface IGetMoneyRequest
{
    public Guid OrderId { get; }
}

public interface IGetMoneyResponse
{
    public Guid OrderId { get; }
}

public class GetMoneyConsumer : IConsumer<IGetMoneyRequest>
{
    #region IConsumer<IGetMoneyRequest> Members

    public Task Consume(ConsumeContext<IGetMoneyRequest> context) =>
        context.RespondAsync<IGetMoneyResponse>(new {context.Message.OrderId});

    #endregion
}

public interface IGetItemsRequest
{
    public Guid OrderId { get; }
}

public interface IGetItemsResponse
{
    public Guid OrderId { get; }
}

public class GetItemsConsumer : IConsumer<IGetItemsRequest>
{
    #region IConsumer<IGetItemsRequest> Members

    public Task Consume(ConsumeContext<IGetItemsRequest> context) =>
        context.RespondAsync<IGetItemsResponse>(new {context.Message.OrderId});

    #endregion
}
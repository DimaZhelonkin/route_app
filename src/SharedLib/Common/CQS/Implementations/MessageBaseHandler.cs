using Ark.SharedLib.Common.Results;
using MassTransit;
using MediatR;

namespace Ark.SharedLib.Common.CQS.Implementations;

public abstract class MessageBaseHandler<TRequest, TResponse> : BaseHandler<TRequest, TResponse>, IConsumer<TRequest>
    where TRequest : class, IRequest<TResponse>
    where TResponse : Result
{
    #region IConsumer<TRequest> Members

    public async Task Consume(ConsumeContext<TRequest> context)
    {
        var messageResult = await Handle(context.Message);
        await context.RespondAsync(messageResult);
    }

    #endregion
}
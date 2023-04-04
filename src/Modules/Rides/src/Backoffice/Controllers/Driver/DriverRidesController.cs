using Ark.Infrastructure.Shared.Controllers;
using Ark.Infrastructure.Shared.Result.AspNetCore;
using Ark.Rides.Application.Features.Driver.Commands.CreateDriverRide;
using Ark.Rides.Application.Features.Driver.Queries.GetListOfPassengers;
using Ark.Rides.Application.Features.Passenger.Queries.GetRecommendedPrice;
using Ark.Rides.Backoffice.Features.Driver.Commands.CancelRide;
using Ark.Rides.Backoffice.Features.Driver.Commands.ConfirmMatch;
using Ark.Rides.Backoffice.Features.Driver.Commands.CreateDriverRide;
using Ark.Rides.Backoffice.Features.Driver.Commands.FinishRide;
using Ark.Rides.Backoffice.Features.Driver.Commands.RejectMatch;
using Ark.Rides.Backoffice.Features.Driver.Commands.SendFeedback;
using Ark.Rides.Backoffice.Features.Driver.Commands.StartRide;
using Ark.Rides.Backoffice.Features.Driver.Queries;
using Ark.Rides.Backoffice.Features.Passenger.Queries.GetRecommendedPriceByRoute;
using Ark.SharedLib.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Rides.Backoffice.Controllers.Driver;

/// <summary>
/// </summary>
[Area("Driver")]
[Route("[area]/rides")]
public class DriverRidesController : ApiBaseController
{
    // public DriverRidesController(TestHandler testHandler)
    // {
    //     
    // }
    /// <summary>
    ///     Получить рекомендуемую цену
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("[action]/{RouteId}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(GetRecommendedPriceResultData), StatusCodes.Status200OK)]
    public async Task<Result<GetRecommendedPriceResultData>> RecommendedPrice(GetRecommendedPriceRequest request,
        CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);

    /// <summary>
    ///     Получить рекомендуемую цену
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("[action]")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(GetRideRequestsResultData), StatusCodes.Status200OK)]
    public async Task<Result<GetRideRequestsResultData>> RideRequests(GetRideRequestsRequest request,
        CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);

    /// <summary>
    ///     Send ride request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<Result> Create([FromBody] CreateDriverRideRequest request,
        CancellationToken cancellationToken) =>
        Mediator.Send(request, cancellationToken);

    /// <summary>
    ///     Начать поездку
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("start/{RideId}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<Result> StartRide(StartRideRequest request, CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);

    /// <summary>
    ///     Завершить поездку
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("finish/{RideId}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<Result> FinishRide(FinishRideRequest request, CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);

    /// <summary>
    ///     Отменить поездку
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("cancel/{RideId}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<Result> CancelRide(CancelRideRequest request, CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);

    /// <summary>
    ///     Подтверждение водителем заявки пассажира
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("confirm/{RideRequestId}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<Result> ConfirmMatch(ConfirmMatchRequest request,
        CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);

    /// <summary>
    ///     Отвергнуть заявку пассажира
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("[action]/{RideRequestId}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<Result> RejectMatch(RejectMatchRequest request, CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);

    /// <summary>
    ///     Отправить отзыв по поездке
    ///     TODO maybe it should be one common request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("[action]/{RideId}/{UserId}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<Result> SendFeedback(SendFeedbackRequest request,
        CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);
}
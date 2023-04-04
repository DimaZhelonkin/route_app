using Ark.Infrastructure.Shared.Controllers;
using Ark.Infrastructure.Shared.Result.AspNetCore;
using Ark.Rides.Application.Features.Passenger.Queries.FindRide;
using Ark.Rides.Application.Features.Passenger.Queries.GetRideInfo;
using Ark.Rides.Backoffice.Features.Driver.Commands.SendFeedback;
using Ark.Rides.Backoffice.Features.Passenger.Commands.CancelMatch;
using Ark.Rides.Backoffice.Features.Passenger.Commands.SendMatch;
using Ark.Rides.Backoffice.Features.Passenger.Queries.FindRide;
using Ark.Rides.Backoffice.Features.Passenger.Queries.GetRideInfo;
using Ark.SharedLib.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Rides.Backoffice.Controllers.Passenger;

/// <summary>
/// </summary>
[Area("Passenger")]
[Route("[area]/rides")]
public class PassengerRidesController : ApiBaseController
{
    /// <summary>
    ///     Получить список поездок по маршруту
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(FindRidesByRouteData), StatusCodes.Status200OK)]
    public async Task<Result<FindRidesByRouteData>> FindRidesByRoute([FromQuery] FindRidesByRouteRequest request,
        CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);

    /// <summary>
    ///     Получить информацию по поездке
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{RideId}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(GetRideInfoResponseData), StatusCodes.Status200OK)]
    public async Task<Result<GetRideInfoResponseData>> RideInfo(GetRideInfoRequest request,
        CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);

    /// <summary>
    ///     Присоединиться к поездке
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{RideId}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<Result> SendMatchRequest(SendMatchRequest request,
        CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);

    /// <summary>
    ///     Отмена посланной заявки
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("[action]/{RideRequestId}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<Result> CancelMatch(CancelMatchRequest request, CancellationToken cancellationToken) =>
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
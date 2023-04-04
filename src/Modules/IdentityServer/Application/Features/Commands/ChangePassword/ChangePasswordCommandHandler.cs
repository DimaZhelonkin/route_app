// using Ark.SharedLib.Common.CQS.Implementations;
// using MediatR;
//
// namespace Ark.IdentityServer.Application.Features.Commands.ChangePassword;
//
// /// <summary>
// /// Change Password command handler
// /// </summary>
// public sealed class ChangePasswordCommandHandler : CommandResultHandler<ChangePasswordCommand>
// {
//     private readonly IMediator _mediator;
//     private readonly IAuthenticatedUserService _userService;
//
//     /// <summary>
//     /// ChangePasswordCommandHandler initializer
//     /// </summary>
//     /// <param name="mediator"></param>
//     /// <param name="userService"></param>
//     public ChangePasswordCommandHandler(
//         IMediator mediator,
//         IAuthenticatedUserService userService
//     )
//     {
//         _mediator = mediator;
//         _userService = userService;
//     }
//
//     /// <summary>
//     /// 
//     /// </summary>
//     /// <param name="request"></param>
//     /// <param name="cancellationToken"></param>
//     /// <returns></returns>
//     public override async Task<Result> Handle(ChangePasswordCommand request,
//         CancellationToken cancellationToken = default)
//     {
//         var changePasswordCommand =
//             new ChangeUserPasswordCommand(request.NewPassword, _userService.Username);
//
//         var result = await _mediator.Send(changePasswordCommand, cancellationToken);
//
//         if (!result.IsSuccess)
//             // _notificationService.ErrorNotification(result.Message);
//             return result;
//
//         // _notificationService.SuccessNotification(_localizer[ResourceKeys.Notifications_PasswordChange_Success]);
//         return result;
//     }
// }


// using System.Security.Claims;
// using Ark.SharedLib.Common.Enumerations;
// using Ark.SharedLib.Common.Extensions;
// using Ark.SharedLib.Domain.Enums;
// using Ark.Identity.Contracts;
// using Ark.Identity.Models;
// using Microsoft.AspNetCore.Http;
//
// namespace Ark.Identity.Services;
//
// public class AuthenticatedUserService : IAuthenticatedUserService
// {
//     public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
//     {
//         var userIdExist =
//             Guid.TryParse(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value,
//                 out var userId);
//         UserId = userIdExist ? userId : null;
//         Username = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? null;
//         Name = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUserClaims.FullNameClaimType)?.Value ?? null;
//         Culture = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUserClaims.CultureClaimType)?.Value ??
//                   null;
//         UiCulture = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUserClaims.UiCultureClaimType)?.Value ??
//                     null;
//         Theme = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUserClaims.ThemeClaimType)?.Value
//             .ToEnum<ThemeEnum>() ?? default;
//         var profilePictureClaim = httpContextAccessor.HttpContext?.User
//             ?.FindFirst(ApplicationUserClaims.ProfilePictureClaimType)?.Value;
//         if (profilePictureClaim != null) ProfilePicture = profilePictureClaim;
//
//         Roles = httpContextAccessor.HttpContext?.User?.Claims.Where(c => c.Type == ClaimTypes.Role)
//             ?.Select(r => r.Value.ToEnum<RolesEnum>()) ?? Array.Empty<RolesEnum>();
//     }
//
//     public string? ProfilePicture { get; }
//     public string? Culture { get; set; }
//     public string? UiCulture { get; set; }
//     public ThemeEnum Theme { get; }
//
//     #region IAuthenticatedUserService Members
//
//     public Guid? UserId { get; }
//
//     public string? Username { get; }
//     public string? Name { get; }
//     public IEnumerable<RolesEnum> Roles { get; }
//
//     #endregion
// }


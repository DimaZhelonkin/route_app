using Refit;

namespace Ark.IdentityServer.Infrastructure.UCaller.Models.Responses;

public class ErrorResponse : BaseResponse
{
    /// <summary>
    ///     Error message from API
    /// </summary>
    [AliasAs("error")]
    public string Error { get; set; }

    /// <summary>
    ///     Error code of response
    /// </summary>
    [AliasAs("code")]
    public ErrorCode Code { get; set; }
}
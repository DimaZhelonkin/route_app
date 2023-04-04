using Ark.StronglyTypedIds;

namespace Ark.IdentityServer.Domain.ApplicationUser;

public record IdentityId(string Value) : StronglyTypedId<string>(Value); 
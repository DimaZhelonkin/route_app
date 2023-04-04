namespace Ark.IdentityServer.Application.DTOs;

/// <summary>
///     Represents a request to update a user's details
/// </summary>
public record UpdateUserDetailsDto
{
    public UpdateUserDetailsDto(string id, string email, string name, string primaryPhoneNumber,
        string secondaryPhoneNumber)
    {
        Id = id;
        Email = email;
        Name = name;
        PrimaryPhoneNumber = primaryPhoneNumber;
        SecondaryPhoneNumber = secondaryPhoneNumber;
    }

    public string Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string PrimaryPhoneNumber { get; set; }
    public string SecondaryPhoneNumber { get; set; }
}
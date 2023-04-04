namespace Ark.Account.Features;

public class ContactsForm
{
    public Guid Id { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? TelegramNickname { get; set; }
    public string? GitUrl { get; set; }
}
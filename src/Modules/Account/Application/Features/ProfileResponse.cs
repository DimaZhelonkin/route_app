namespace Ark.Account.Features;

public class ProfileResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public ContactsForm Contacts { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }

    /// <summary>
    ///     Отчество
    /// </summary>
    public string? Patronymic { get; set; }

    public DateOnly? BirthDate { get; set; }
    public string? Description { get; set; }

    /// <summary>
    ///     Должность
    /// </summary>
    public string? Position { get; set; }

    public string? Competencies { get; set; }
}
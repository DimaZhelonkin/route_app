using System.Text.RegularExpressions;
using Ark.SharedLib.Common.Results;
using Ark.SharedLib.Common.Results.Extensions;
using FluentValidation;

namespace Ark.SharedLib.Domain.ValueObjects.Emails;

// public class Email : ValueObject
// {
//     private long Id { get; set; }
//
//     [Display(Name = "Content")]
//     [Required(AllowEmptyStrings = false)]
//     private string Content { get; set; }
//
//     [Display(Name = "DefaultTemplate")]
//     private bool IsDefault { get; set; }
//
//     //TemplateType? TemplateType { get; set; }
//
//     protected override IEnumerable<object> GetAtomicValues()
//     {
//         yield return Id;
//         yield return Content;
//         yield return IsDefault;
//     }
// }
public sealed class Email : ValueObject
{
    /// <summary>
    ///     The email maximum length.
    /// </summary>
    public const int MaxLength = 256;

    private const string EmailRegexPattern =
        @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    private static readonly Lazy<Regex> EmailFormatRegex =
        new(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

    /// <summary>
    ///     Initializes a new instance of the <see cref="Email" /> class.
    /// </summary>
    /// <param name="value">The email value.</param>
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; set; }

    public static implicit operator Email(string email) => Create(email).ThrowIfFailure().Value!;

    public static implicit operator string(Email email) => email.Value;
    public override string ToString() => Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<Email> Create(string email) =>
        // TODO to rullset
        Result.Create(email)
              .Ensure(f => !string.IsNullOrWhiteSpace(f), new Error("Email", "Couldn't be empty"))
              // .Ensure(f => new EmailValidator().Validate(email).IsValid, new Error("Email", "Not email"))
              .Ensure(e => EmailFormatRegex.Value.IsMatch(e), new Error("Email", "Not email"))
              .Map(f => new Email(f));

    #region Nested type: EmailValidator

    private sealed class EmailValidator : AbstractValidator<string>
    {
        public EmailValidator()
        {
            RuleFor(email => email).EmailAddress();
        }
    }

    #endregion
}
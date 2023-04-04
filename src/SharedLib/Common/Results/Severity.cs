using System.ComponentModel.DataAnnotations;

namespace Ark.SharedLib.Common.Results;

public enum Severity
{
    [Display(Name = nameof(Error))] Error = 0,
    [Display(Name = nameof(Warning))] Warning = 1,
    [Display(Name = nameof(Info))] Info = 2,
}
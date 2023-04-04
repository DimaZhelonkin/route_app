namespace Ark.SharedLib.Application.Abstractions.Shared;

public interface IPasswordGenerator
{
    string GenerateRandomPassword();
}
// Contracts/ITokenService.cs

using BackendApi.Models;

public interface ITokenService
{
    string GenerateJwtToken(User user);
}
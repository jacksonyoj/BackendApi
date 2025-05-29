// Contracts/Auth/IGuardService.cs

using System.Security.Claims;

public interface IGuardService
{
    ServiceResult CheckUserClaims(ClaimsPrincipal user);
}
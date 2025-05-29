// Service/Auth/GuardService.cs

using System.Security.Claims;

public class GuardService : IGuardService
{
    public ServiceResult CheckUserClaims(ClaimsPrincipal user)
    {
        var result = new ServiceResult();

        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            result.Errors.Add("未驗證的請求");
            return result;
        }
        // BUG: 取不到值
        var userId = user.FindFirst("userId")?.Value;
        Console.WriteLine($"這邊是userId:{userId}");
        var userName = user.FindFirst(ClaimTypes.Name) ?? user.FindFirst("sub");
        Console.WriteLine($"這邊是userName:{userName}");

        if (string.IsNullOrEmpty(userId) || userName == null)
        {
            result.Errors.Add("Token 內容錯誤");
            return result;
        }
        result.Data = new
        {
            UserId = userId,
            UserName = userName!.Value
        };
        return result;
    }
}
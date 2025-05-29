// Contracts/IAuthService.cs

public interface IAuthService
{
    /// <summary>
    /// 驗證帳號密
    /// </summary>
    /// <param name="username">帳號</param>
    /// <param name="password">密碼</param>
    /// <returns>正確，或錯訊息</returns>
    Task<ServiceResult> ValidateCredentialsAsync(string username, string password);
}

// Service/AuthService.cs

using BackendApi.Data;
using Microsoft.EntityFrameworkCore;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    public AuthService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ServiceResult> ValidateCredentialsAsync(string username, string password)
    {
        var result = new ServiceResult();
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == username);
        if (user == null)
        {
            result.Errors.Add("帳號不存在");
            return result;
        }
        bool isPwCorrect = BCrypt.Net.BCrypt.Verify(password, user!.PasswordHash);
        if (!isPwCorrect)
        {
            result.Errors.Add("密碼錯誤");
            return result;
        }
        result.Data = user;
        return result;
    }

}
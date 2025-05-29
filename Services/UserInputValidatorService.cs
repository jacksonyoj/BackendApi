// Service/UserInputValidatorServiecr.cs
using BackendApi.DTOs;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

/// <summary>
/// 這邊是驗證輸入帳號/密碼的字元限制
/// </summary>
public class UserInputValidatorService : IUserInputValidatorService
{

    private const int maxNameLength = 15;
    private const int minNameLength = 3;
    private const int maxPwLength = 20;
    private const int minPwLength = 4;

    public ServiceResult ValidateUserName(string username)
    {

        var result = new ServiceResult();

        if (string.IsNullOrWhiteSpace(username))
        {
            result.Errors.Add("帳號不能空白");
        }

        if (username.Length > maxNameLength)
        {
            result.Errors.Add($"長度超過{maxNameLength}字元");
        }

        return result;
    }

    public ServiceResult ValidatePassword(string password)
    {
        var result = new ServiceResult();
        if (string.IsNullOrWhiteSpace(password))
        {
            result.Errors.Add("密碼不得為空");
        }

        if (password.Length > maxPwLength)
        {
            result.Errors.Add($"密碼長度超過{maxPwLength}字元");
        }
        return result;
    }


    public ServiceResult ErrorUserList(List<RegisterDTO> dtos)
    {
        /// <summary>
        /// 批量驗證帳號密碼
        /// </summary>
        /// <returns>回傳錯誤的</returns>
        var result = new ServiceResult();
        foreach (var dto in dtos)
        {
            var nameResult = ValidateUserName(dto.UserName);
            if (!nameResult.Success)
            {
                result.Errors.AddRange(nameResult.Errors.Select(err => $"帳號 {dto.UserName} 錯誤: {err}"));
            }

            var pwResult = ValidatePassword(dto.Password);
            if (!pwResult.Success)
            {
                result.Errors.AddRange(pwResult.Errors.Select(err => $"帳號 {dto.UserName} 密碼錯誤: {err}"));
            }
        }
        return result;
    }

    public ServiceResult ValidateLoginInput(LoginDTO dto)
    {
        var result = new ServiceResult();

        var nameResult = ValidateUserName(dto.UserName);
        if (!nameResult.Success)
        {
            result.Errors.AddRange(nameResult.Errors.Select(err => $"帳號 {dto.UserName} 錯誤: {err}"));
        }

        var pwResult = ValidatePassword(dto.Password);
        if (!pwResult.Success)
        {
            result.Errors.AddRange(pwResult.Errors.Select(err => $"帳號 {dto.UserName} 密碼錯誤: {err}"));
        }

        return result;
    }
}
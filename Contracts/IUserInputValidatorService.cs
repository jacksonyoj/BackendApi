// Constracts/IUserInputValidatorService.cs
using BackendApi.Models;
using BackendApi.DTOs;

public interface IUserInputValidatorService
{
    ServiceResult ValidateUserName(string username);
    ServiceResult ValidatePassword(string password);
    /// <summary>
    /// (批量)驗證多筆帳號密碼，回傳所以錯誤訊息
    /// </summary>
    /// <param name="dtos">所有前端來的[]列表</param>
    /// <returns>所有錯誤清單List</returns>
    ServiceResult ErrorUserList(List<RegisterDTO> dtos);

    ServiceResult ValidateLoginInput(LoginDTO dto);
}
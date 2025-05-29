// Constracts/IUserService.cs

using BackendApi.DTOs;
using BackendApi.Models;
public interface IUserService
{
    /// <summary>
    /// (批量)驗證帳號是否存在
    /// </summary>
    /// <param name="users">批量輸入資料[]</param>
    /// <returns>重複帳號列表[]</returns>
    Task<ServiceResult> ValidateUsersAsync(List<RegisterDTO> users);

    /// <summary>
    /// 建立使用者
    /// </summary>
    /// <param name="users">要建立的註冊資料列表</param>
    Task<ServiceResult> RegisterUserAsync(List<RegisterDTO> users);
}
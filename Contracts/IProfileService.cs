// Constracts/IProfileService.cs
using BackendApi.DTOs;
// Profile 的 CRUD
public interface IProfileService
{
    Task<ServiceResult> GetAllProfileAsync();

}
using Microsoft.EntityFrameworkCore;
using BackendApi.Data;
using BackendApi.Models;
using Microsoft.VisualBasic;
using BackendApi.DTOs;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    public UserService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ServiceResult> ValidateUsersAsync(List<RegisterDTO> dtos)
    {
        var result = new ServiceResult();

        foreach (var dto in dtos)
        {
            bool exist = await _db.Users.AnyAsync(u => u.UserName == dto.UserName);
            if (exist)
            {
                result.Errors.Add($"帳號{dto.UserName} 已存在");
            }
        }
        return result;
    }

    public async Task<ServiceResult> RegisterUserAsync(List<RegisterDTO> dtos)
    {
        var result = new ServiceResult();
        try
        {
            var newUsers = dtos.Select(dto => new User
            {
                UserName = dto.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreateAt = DateTime.UtcNow,
                Profile = new Profile
                {
                    Email = "",
                    Phone = "",
                    AddressDetail = "",
                    DistrictId = null
                }
            }).ToList();

            _db.Users.AddRange(newUsers);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            result.Errors.Add($"資料庫錯誤 {ex.Message}");
        }
        return result;
    }

}
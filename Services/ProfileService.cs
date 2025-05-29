// Services/ProfileService.cs

using BackendApi.Data;
using BackendApi.DTOs;
using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

public class ProfileService : IProfileService
{
    private readonly AppDbContext _db;
    public ProfileService(AppDbContext db)
    {
        _db = db;
    }
    public async Task<ServiceResult> GetAllProfileAsync()
    {
        var result = new ServiceResult();

        try
        {
            var profiles = await _db.Profiles
                .Include(p => p.User)
                .Include(p => p.District)
                .ThenInclude(d => d.City)
                .Select(p => new ProfileDTO
                {
                    Id=p.Id,
                    UserName = p.User.UserName,
                    Email = p.Email,
                    Phone = p.Phone,
                    CityId = p.District.City.Id,
                    DistrictId = p.District.Id,
                    AddressDetail = p.AddressDetail

                }).ToListAsync();
            result.Data = profiles;
        }
        catch (Exception ex)
        {
            result.Errors.Add($"查詢錯誤:{ex.Message}");
        }
        return result;
    }
    

}
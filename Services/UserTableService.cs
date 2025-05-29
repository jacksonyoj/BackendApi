// Service/UserTable.cs

using System.Runtime.InteropServices;
using BackendApi.Data;
using Microsoft.EntityFrameworkCore;

public class UserTableService : IUserTableService
{
    private readonly AppDbContext _db;
    public UserTableService(AppDbContext db)
    {
        _db = db;
    }
    public async Task<ServiceResult> GetCityList()
    {
        var result = new ServiceResult();
        try
        {
            var cities = await _db.Cities
                .Select(c => new CityDTO{
                    Id = c.Id,
                    CityName=c.CityName
                }).ToListAsync();

            result.Data = cities;
        }
        catch (Exception ex)
        {
            result.Errors.Add($"取得城市列表錯誤{ex.Message}");
        }
        return result;
    }

    public async Task<ServiceResult> GetDistrictList()
    {
        var result = new ServiceResult();
        try
        {
            var districts = await _db.Districts
                .Select(d => new DistrictDTO
                {
                    Id = d.Id,
                    CityId = d.CityId,
                    DistrictName = d.DistrictName
                }).ToListAsync();
            result.Data = districts;
        }
        catch (Exception ex)
        {
            result.Errors.Add($"取得區域列表錯誤{ex.Message}");
        }
        return result;
    }
}
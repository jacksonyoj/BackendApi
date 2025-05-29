// Controllers/UserManager.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackendApi.Models;
using BackendApi.DTOs;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserManagerController : ControllerBase
{
    private readonly IProfileService _profileService;
    private readonly IUserTableService _userTableService;

    public UserManagerController(
        IProfileService profileService,
        IUserTableService userTableService
    )
    {
        _profileService = profileService;
        _userTableService = userTableService;

    }


    [HttpGet]
    public async Task<IActionResult> GetProfiles()
    {
        var getResult = await _profileService.GetAllProfileAsync();
        if (!getResult.Success)
        {
            return BadRequest(new { message = "查詢失敗", errors = getResult.Errors });
        }
        var profiles = getResult.Data as List<ProfileDTO>;
        return Ok(profiles);
    }


    [HttpGet("city")]
    public async Task<IActionResult> GetCity()
    {
        var cityResult = await _userTableService.GetCityList();
        if (!cityResult.Success)
        {
            return BadRequest(new { message = "查詢失敗", errors = cityResult.Errors });

        }
        var cities = cityResult.Data as List<CityDTO>;

        return Ok(cities);
    }
    [HttpGet("district")]
    public async Task<IActionResult> GetDistrict()
    {
        var districtResult = await _userTableService.GetDistrictList();
        if (!districtResult.Success)
        {
            return BadRequest(new { message = "查詢失敗", errors = districtResult.Errors });

        }
        var districts = districtResult.Data as List<DistrictDTO>;

        return Ok(districts);
    }
}
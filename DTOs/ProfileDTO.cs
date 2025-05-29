// DTOs/UserManageDTO.cs

using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BackendApi.DTOs;

public class ProfileDTO
{
    public int Id{ get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int? CityId { get; set; }
    public int? DistrictId { get; set; }
    public string AddressDetail { get; set; } = string.Empty;
}
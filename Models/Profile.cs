// Models/Profiles.cs

namespace BackendApi.Models;

public class Profile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? DistrictId { get; set; }


    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string AddressDetail { get; set; } = string.Empty;

    // one-to-one relationship with User
    public User User { get; set; } = null!;
    // one-to-one relationship with District
    public District District { get; set; } = null!;
}

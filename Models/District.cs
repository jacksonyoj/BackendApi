// Models/District.cs
using System.Net.NetworkInformation;

namespace BackendApi.Models;

public class District
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public string DistrictName { get; set; } = string.Empty;

    public City City { get; set; } = null!;
    public ICollection<Profile> Profiles { get; set; } = new List<Profile>();

}
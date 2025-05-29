// Models/City.cs

namespace BackendApi.Models;

public class City
{
    public int Id { get; set; }
    public string CityName { get; set; } = string.Empty;

    public ICollection<District> Districts { get; set; } = new List<District>();
}
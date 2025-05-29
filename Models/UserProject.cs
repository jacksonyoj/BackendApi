// Models/User_Project.cs
namespace BackendApi.Models;

public class UserProject
{
    public int UserId { get; set; }
    public int ProjectId { get; set; }

    public User User { get; set; } = null!;
    public Project Project { get; set; } = null!;
}
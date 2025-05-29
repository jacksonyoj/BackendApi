// Models/User.cs

namespace BackendApi.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; } = DateTime.Now;

    public Profile? Profile { get; set; }

}


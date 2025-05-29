// DTOs/LoginDTO.cs

namespace BackendApi.DTOs;

public class LoginDTO
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
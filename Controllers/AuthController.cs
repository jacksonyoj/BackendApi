// Controller/AuthController.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IGuardService _guardService;
    public AuthController(IGuardService guardService)
    {
        _guardService = guardService;
    }

    [HttpGet("guard")]
    [Authorize]
    public IActionResult GuardChck()
    {
        // BUG: username 取不到值
        
        // var guardResult = _guardService.CheckUserClaims(User);
        // if (!guardResult.Success)
        // {
        //     return Unauthorized(new { message = "Token 驗證失敗", errors = guardResult.Errors });
        // }
        return Ok(new
        {
            message = "Token 驗證成功",
            // user = guardResult.Data
        });
    }
}  
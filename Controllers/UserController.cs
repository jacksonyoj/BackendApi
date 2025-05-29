//  Controller/UserController.cs

using BackendApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackendApi.Models;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserInputValidatorService _userInputValidatorService;
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    public UserController(
        IUserInputValidatorService userInputValidatorService,
        IUserService userService,
        IAuthService authService,
        ITokenService tokenService
    )
    {
        _userInputValidatorService = userInputValidatorService;
        _userService = userService;
        _authService = authService;
        _tokenService = tokenService;
    }

    /// <summary>
    /// 添加使用者的api
    /// </summary>
    /// <param name="dtos">批量輸入的user清單</param>
    /// <returns>回傳添加結果，或錯誤</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] List<RegisterDTO> dtos)
    {
        if (dtos == null || dtos.Count == 0)
        {
            return BadRequest(new { message = "至少輸入一筆資料" });
        }

        // Step.1 驗證格式 (UserInputValidatorService)
        var formatResult = _userInputValidatorService.ErrorUserList(dtos);
        if (!formatResult.Success)
        {
            return BadRequest(new { message = "資料格式失敗", errors = formatResult.Errors });
        }

        // Step.2 重複驗證 (UserService)
        var duplicateResult = await _userService.ValidateUsersAsync(dtos);
        if (!duplicateResult.Success)
        {
            return BadRequest(new { message = "帳號重複", errors = duplicateResult.Errors });
        }

        // Step.3 批量寫入(UserService)
        var registerResult = await _userService.RegisterUserAsync(dtos);
        if (!registerResult.Success)
        {
            return StatusCode(500, new { message = "添加失敗", errors = registerResult.Errors });
        }
        return Ok(new { message = $"添加成功,共 {dtos.Count}" });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        // Step.1 驗證格式 (UserInputValidatorService)
        var inputValidatorResult = _userInputValidatorService.ValidateLoginInput(dto);
        if (!inputValidatorResult.Success)
        {
            return BadRequest(new { massage = "格式錯誤", errors = inputValidatorResult.Errors });
        }
        // Step.2 驗證帳密(AuthService)
        var authResult = await _authService.ValidateCredentialsAsync(dto.UserName, dto.Password);
        if (!authResult.Success)
        {
            return BadRequest(new { message = "登入失敗", errors = authResult.Errors });
        }

        var user = (User)authResult.Data!;
        var token = _tokenService.GenerateJwtToken(user);
        return Ok(new { message = "登入成功", token = token });
    }


}
using CodeBattles_Backend.Domain.DTOs;
using CodeBattles_Backend.DTOs;
using CodeBattles_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeBattles_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly AuthService _authService;
  private readonly JWTService _jwTService;
  private readonly PasswordService _passwordService;


  public AuthController(AuthService authService, JWTService jWTService, PasswordService passwordService)
  {
    _authService = authService;
    _jwTService = jWTService;
    _passwordService = passwordService;
  }
  [HttpPost("login")]
  public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
  {
    var existingUser = await _authService.GetUserByEmail(loginDTO.Email);
    if (existingUser == null) return BadRequest("Email and Password combination is incorrect!!");
    var isPasswordMatch = _passwordService.ComparePassword(new UserJwtDTO { Email = loginDTO.Email }, existingUser.Password, loginDTO.Password);
    if (!isPasswordMatch) return BadRequest("Email and Password combination is incorrect!!");
    string token = _jwTService.GenerateJwtToken(existingUser);
    return Ok(new { token = token });
  }
  [HttpPost("register")]
  public async Task<ActionResult> Register([FromBody] RegisterDTO registerDTO)
  {
    var createdUser = await _authService.RegisterUser(registerDTO);
    if (createdUser == null) return BadRequest("User with Email already exist!");
    string token = _jwTService.GenerateJwtToken(createdUser);
    return Ok(new { token = token });
  }

}
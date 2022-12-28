using api_bi.DTOs;
using api_bi.Models;
using api_bi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace api_bi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    ILoginService _loginService;
    private IConfiguration config;

    public LoginController(ILoginService loginService, IConfiguration configuration)
    {
        _loginService = loginService;
        config = configuration;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Login(UserDto userDto)
    {
        var user = await _loginService.GetUser(userDto);

        if (user is null)
            return BadRequest(new { message = "Invalid credentalias" });

        // generate token

        string jwtToken = GenerateToken(user);

        return Ok(new { token = jwtToken });
    }

    private string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.name!),
            new Claim(ClaimTypes.Email, user.email!),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds
        );

        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }

}
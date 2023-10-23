using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using examplemvc.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore; 
using System.Linq;
using examplemvc.Data;

[Route("api/jwt")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext; 

    public AuthController(IConfiguration configuration, ApplicationDbContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromHeader] Login loginModel)
    {
        var user = AuthenticateUser(loginModel.Username, loginModel.Password);

        if (user != null)
        {
            var token = GenerateJwtToken(user.Username);
            return Ok(new { Token = token });
        }
        else
        {
            return Unauthorized();
        }
    }

    private User AuthenticateUser(string username, string password)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        return user;
    }

    private string GenerateJwtToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: new[]
            {
                new Claim("username", username)
            },
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"])),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}

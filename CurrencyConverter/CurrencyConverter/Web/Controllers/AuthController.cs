using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CurrencyConverter.Data;
using CurrencyConverter.Data.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CurrencyConverter.Web.Controllers
{
  [Route("auth")]
  public class AuthController
  {
    private IConfiguration _config;
    private IDbContext _dbContext;
    
    public AuthController(IConfiguration config, IDbContext dbContext)
    {
      _config = config;
      _dbContext = dbContext;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public IActionResult CreateToken([FromQuery]LoginModel login)
    {
      IActionResult response = new UnauthorizedResult();
      var user = Authenticate(login);

      if (user != null)
      {
        var tokenString = BuildToken(user);
        response = new JsonResult(new { token = tokenString });
      }

      return response;
    }
    
    private string BuildToken(User user)
    {
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
        _config["Jwt:Issuer"],
        _config["Jwt:Issuer"],
        expires: DateTime.Now.AddMinutes(30),
        claims: new List<Claim>()
        {
          new Claim("username", user.UserName)
        },
        signingCredentials: creds);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private User Authenticate(LoginModel login)
    {
      string hashedPassword;
      using(var sha256 = SHA256.Create())  
      {  
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes("hello world"));  
        hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();  
      }

      var user = _dbContext.Set<User>()
        .FirstOrDefault(x => x.UserName == login.UserName && x.PasswordHash == hashedPassword);
      
      return user;
    }
  }
}
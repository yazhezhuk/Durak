using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Durak.Client.Models;
using Durak.Core.GameModels.Players;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Durak.Client.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly UserManager<Player> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private IConfiguration _configuration;


	public AuthController(UserManager<Player> userManager, RoleManager<IdentityRole> roleManager)
	{
		_roleManager = roleManager;
		_userManager = userManager;
	}


	[HttpPost("")]
	public async Task<IActionResult> Auth([FromBody] LoginModel loginModel)
	{
		var existingUserModel = await _userManager.FindByNameAsync(loginModel.Username);
		var passwordCheckResult = await _userManager.CheckPasswordAsync(existingUserModel, loginModel.Password);

		if (!passwordCheckResult)
			return Unauthorized();

		return Ok(new
		{
			token = GenerateJwtToken(existingUserModel)
		});
	}

	private string GenerateJwtToken(Player player)
	{
		// generate token that is valid for 7 days
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("somesecretkey32228!"));

		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken("http://localhost:3000/",
			"http://localhost:3000/",
			null,
			expires: DateTime.Now.AddMinutes(120),
			signingCredentials: credentials);


		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
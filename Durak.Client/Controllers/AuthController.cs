using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Durak.Client.Models;
using Durak.Core;
using Durak.Core.GameModels.Players;
using Durak.Core.Interfaces;
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
	private readonly UserManager<AppUser> _userManager;
	private readonly IGameSessionRepository _gameSessionRepository;
	private readonly RoleManager<IdentityRole> _roleManager;
	private IConfiguration _configuration;


	public AuthController(IGameSessionRepository gameSessionRepository,
		UserManager<AppUser> userManager,
		RoleManager<IdentityRole> roleManager)
	{
		_gameSessionRepository = gameSessionRepository;
		_roleManager = roleManager;
		_userManager = userManager;
	}


	[HttpPost("")]
	public async Task<IActionResult> Auth([FromBody] LoginModel loginModel)
	{
		var existingUserModel = await _userManager.FindByNameAsync(loginModel.Username);

		if (existingUserModel == null)
		{
			var user = new AppUser { UserName = loginModel.Username};

			var result = await _userManager.CreateAsync(user, loginModel.Password);
				if (!result.Succeeded)
					return BadRequest();
				existingUserModel = user;
		}

		_userManager.AddClaimAsync(existingUserModel, new Claim(ClaimTypes.Name, existingUserModel.UserName));
		var passwordCheckResult = await _userManager.CheckPasswordAsync(existingUserModel, loginModel.Password);

		if (!passwordCheckResult)
			return Unauthorized();

		return Ok(new
		{
			token = GenerateJwtToken(existingUserModel)
		});
	}

	private string GenerateJwtToken(AppUser appUser)
	{
		// generate token that is valid for 7 days
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Helper.ApplicationOptions.DEFAULT_SECRET));

		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
			new Claim(ClaimTypes.Name, appUser.UserName)
		};


		var token = new JwtSecurityToken(Helper.ApplicationOptions.DEFAULT_HOST,
			Helper.ApplicationOptions.DEFAULT_HOST,
			claims,
			expires: DateTime.Now.AddDays(120),
			signingCredentials: credentials);


		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}

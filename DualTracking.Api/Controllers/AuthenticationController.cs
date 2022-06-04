using DualTracking.Database;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DualTracking.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{

		public readonly SignInManager<Parent> _signInManager;
		public readonly UserManager<Parent> _userManager;
		private readonly ApplicationDbContext _context;

		public AuthenticationController(SignInManager<Parent> signInManager, UserManager<Parent> userManager, ApplicationDbContext context)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_context = context;
		}

		[HttpPost("login")]
		public async Task<IActionResult> LoginAsync(LoginViewModel parameters)
		{
			var result = await _signInManager.PasswordSignInAsync(parameters.UserName, parameters.Password, false, false);

			var user = await _userManager.FindByNameAsync(parameters.UserName);
			await _context.Entry(user).Collection(x => x.Children).LoadAsync();
			
			//var token = await _userManager.GenerateUserTokenAsync(user, "Default", "Token");

			//var u = await _userManager.GetUserAsync(HttpContext.User);
			
			//var auth = await _userManager.GetAuthenticationTokenAsync(user, "Default", "Token");

			if (result.Succeeded)
			{
				var claims = new[]
						{
						  new Claim(JwtRegisteredClaimNames.Sub, user.Id),
						  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
						};

				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DEF74CFD-1743-4438-8DF1-0F3056406508"));
				var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

				var token = new JwtSecurityToken("https://localhost:7049",
				  "DualTrackers",
				  claims,
				  expires: DateTime.Now.AddMonths(1),
				  signingCredentials: creds);

				return Ok(new { Id = user.Id, UserName = user.UserName, PhoneNumber = user.PhoneNumber, Address = user.Address, Children = user.Children});
			}

			return BadRequest(new { reasonPhrase = "Invalid username or password" });
		}

		
		[HttpGet("Values")]
		public string[] Values()
		{
			return new string[] { "value1", "value2" };
		}
	}
}

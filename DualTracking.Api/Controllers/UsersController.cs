using DualTracking.Database;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DualTracking.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly UserManager<Parent> _userManager;
		private readonly SignInManager<Parent> _signInManager;
		private readonly ApplicationDbContext _context;

		public UsersController(UserManager<Parent> userManager, SignInManager<Parent> signInManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
		}

		[HttpPost("SignUp")]
		public async Task<IActionResult> SignUpAsync([FromBody] SignUpViewModel parameters)
		{
			var user = new Parent
			{
				UserName = parameters.UserName,
				Address = parameters.Address,
				PhoneNumber = parameters.PhoneNumber
			};

			var result = await _userManager.CreateAsync(user, parameters.Password);

			if (result.Succeeded)
			{
				var current = await _userManager.FindByNameAsync(parameters.UserName);
				return Ok(current);
			}

			return BadRequest(result.Errors);
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] Parent parent)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
			{
				return NotFound();
			}

			user.UserName = parent.UserName;
			user.Address = parent.Address;
			user.PhoneNumber = parent.PhoneNumber;
			
			var result = await _userManager.UpdateAsync(user);

			if (result.Succeeded)
			{
				return Ok();
			}

			return BadRequest(result.Errors);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUserAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
			{
				return NotFound();
			}

			var result = await _userManager.DeleteAsync(user);

			if (result.Succeeded)
			{
				return Ok();
			}

			return BadRequest(result.Errors);
		}

		[HttpDelete("DeleteAll")]
		public async Task<IActionResult> DeleteAllUsersAsync()
		{
			try
			{
				_context.Users.RemoveRange(_context.Users);
				await _context.SaveChangesAsync();
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message, innerException = ex.InnerException });
			}
		}
	}
}

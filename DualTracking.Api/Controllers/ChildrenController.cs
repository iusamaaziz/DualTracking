using DualTracking.Database;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DualTracking.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChildrenController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public ChildrenController(ApplicationDbContext context)
		{
			_context = context;
		}
		
		[HttpPost("Add")]
		public async Task<IActionResult> Post([FromBody] ChildViewModel child)
		{
			try
			{
				Child c = new Child()
				{
					Name = child.Name,
					DateOfBirth = child.DateOfBirth,
					Gender = child.Gender,
					ParentId = child.ParentId
				};
				_context.Children.Add(c);
				await _context.SaveChangesAsync();
				return Ok(c);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] ChildViewModel model)
		{
			var childToUpdate = await _context.Children.FindAsync(id);

			if (childToUpdate == null || childToUpdate.Id != model.Id)
			{
				return BadRequest();
			}

			childToUpdate.Name = model.Name;
			childToUpdate.DateOfBirth = model.DateOfBirth;
			childToUpdate.Gender = model.Gender;
			

			_context.Entry(childToUpdate).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return Ok(childToUpdate);
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetChildren()
		{
			var children = await _context.Children.ToListAsync();
			return Ok(children);
		}

		[HttpGet("GetByParentId/{id}")]
		public async Task<IActionResult> GetChildrenByParentId(string id)
		{
			var children = await _context.Children.Where(c => c.ParentId == id).ToListAsync();
			return Ok(children);
		}

		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var child = await _context.Children.FindAsync(id);
			if (child == null)
			{
				return NotFound();
			}

			_context.Children.Remove(child);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete("DeleteAll")]
		public async Task<IActionResult> DeleteAll()
		{
			var children = await _context.Children.ToListAsync();
			_context.Children.RemoveRange(children);
			await _context.SaveChangesAsync();
			return Ok();
		}
	}
}

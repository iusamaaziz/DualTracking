using DualTracking.Database;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DualTracking.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ResponsesController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public ResponsesController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost("Add")]
		[Authorize]
		public async Task<IActionResult> Post([FromBody] ResponseModel response)
		{
			if (response == null)
			{
				return BadRequest();
			}

			Response respons = new()
			{
				Id = response.Id,
				QuestionnaireId = response.QuestionnaireId,
				ChildId = response.ChildId,
				Value = response.Value
			};
			
			_context.Responses.Add(respons);
			await _context.SaveChangesAsync();

			return Ok();
		}

		[HttpPost("AddList")]
		[Authorize]
		public async Task<IActionResult> Post([FromBody] ResponseModel[] responses)
		{
			if (responses == null)
			{
				return BadRequest();
			}

			_context.Responses.AddRange(Mapper.Map(responses));
			await _context.SaveChangesAsync();

			return Ok();
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			var responses = await _context.Responses.ToListAsync();

			return Ok(responses);
		}

		//get last response to every question for every child
		[HttpGet("GetLastResponsesForAllChildren")]
		public async Task<IActionResult> GetLastResponseAsync()
		{
			var responses = await _context.Responses.GroupBy(r => new { r.ChildId, r.QuestionnaireId })
				.Select(g => new { g.Key.ChildId, g.Key.QuestionnaireId, Value = g.OrderByDescending(r => r.Date).First().Value })
				.ToListAsync();

			return Ok(responses);
		}

		[HttpGet("GetLastResponseByChildId/{childId}")]
		public async Task<IActionResult> GetLastResponseByChildIdAsync(int childId)
		{
			var responses = await _context.Responses.Where(r => r.ChildId == childId)
				.GroupBy(r => new { r.QuestionnaireId })
				.Select(g => new { g.Key.QuestionnaireId, Value = g.OrderByDescending(r => r.Date).First().Value })
				.ToListAsync();

			return Ok(responses);
		}

		[HttpGet("GetByChildId/{childId}")]
		public async Task<IActionResult> GetByChildIdAsync(int childId)
		{
			var responses = await _context.Responses.Where(r => r.ChildId == childId).ToListAsync();

			return Ok(responses);
		}

		[HttpGet("GetByQuestionId/{questionId}")]
		public async Task<IActionResult> GetByQuestionIdAsync(int questionId)
		{
			var responses = await _context.Responses.Where(r => r.QuestionnaireId == questionId).ToListAsync();

			return Ok(responses);
		}

		[HttpDelete("DeleteAll")]
		public async Task<IActionResult> DeleteAllAsync()
		{
			_context.Responses.RemoveRange(_context.Responses);
			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}

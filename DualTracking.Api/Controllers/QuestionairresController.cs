using DualTracking.Database;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DualTracking.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuestionairresController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public QuestionairresController(ApplicationDbContext context)
		{
			_context = context;
		}
		
		
		private async Task EnsureQuestionairreCreated()
		{
			if (!await _context.Questionnaires.AnyAsync())
			{
				Questionnaire[] array = new[]
				{
					new Questionnaire
					{
						Title = "Cough",
						Description = "How severe was the cough today?",
						Frequency = 1
					},
					new Questionnaire
					{
						Title = "Fever",
						Description = "How severe was the fever today?",
						Frequency = 1
					},
					new Questionnaire
					{
						Title = "Cervical Lymph Nodes",
						Description = "Have you noticed an increase in the size of the glands around the neck?",
						Frequency = 7
					},
					new Questionnaire
					{
						Title = "Sweat",
						Description = "How much did he/she sweat last night?",
						Frequency = 1
					},
					new Questionnaire
					{
						Title = "Appetite",
						Description = "How badly was the appetite of a child affected last week?",
						Frequency = 7
					},
					new Questionnaire
					{
						Title = "Abdominal pain",
						Description = "How badly a child experienced abdominal pain during the past week?",
						Frequency = 7
					},
					new Questionnaire
					{
						Title = "Weight gain",
						Description = "How difficult was it for a kid to gain weight in past 15 days?",
						Frequency = 14
					},
					new Questionnaire
					{
						Title = "Playfulness",
						Description = "How playful was your kid today?",
						Frequency = 1
					},
					new Questionnaire
					{
						Title = "Anxiety",
						Description = "How anxious was your child today?",
						Frequency = 1
					},
					new Questionnaire
					{
						Title = "Sleep quality",
						Description = "How was the quality of sleep last night?",
						Frequency = 1
					},
					new Questionnaire
					{
						Title = "Care burden",
						Description = "How much of a burden did you feel today?",
						Frequency = 1
					},
					new Questionnaire
					{
						Title = "Exposure to SHS",
						Description = "How much your child was exposed to cigarette smoke today?",
						Frequency = 1
					},
					new Questionnaire
					{
						Title = "Exposure to cooking fuel burning",
						Description = "How much your child was exposed to cooking fuel burning today?",
						Frequency = 1
					},
					new Questionnaire
					{
						Title = "Exposure to traffic pollution",
						Description = "How much your child was exposed to pollution caused by road traffic today?",
						Frequency = 1
					},
					new Questionnaire
					{
						Title = "Overall exposure",
						Description = "How much your child was exposed to air pollutants last week?",
						Frequency = 7
					},
				};

				await _context.Questionnaires.AddRangeAsync(array);
				await _context.SaveChangesAsync();
			}
		}
		
		[HttpGet]
		public async Task<IActionResult> GetAsync()
		{
			await EnsureQuestionairreCreated();
			return Ok(await _context.Questionnaires.ToListAsync());
		}
	}
}

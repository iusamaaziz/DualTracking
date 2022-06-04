using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DualTracking.Database
{
	public class Response
	{
		public int Id { get; set; }
		public int Value { get; set; }
		public DateTime Date { get; set; }

		public int QuestionnaireId { get; set; }
		public int ChildId { get; set; }

		public Questionnaire? Questionnaire { get; set; }
		public Child? Child { get; set; }
	}
}

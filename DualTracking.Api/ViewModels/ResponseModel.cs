namespace DualTracking.Api
{
	public class ResponseModel
	{
		public int Id { get; set; }
		public int Value { get; set; }
		public DateTime Date { get; set; }

		public int QuestionnaireId { get; set; }
		public int ChildId { get; set; }
	}
}

using DualTracking.Database;

namespace DualTracking.Api
{
	public class Mapper
	{
		public static Response Map(ResponseModel responseModel)
		{
			Response response = new()
			{
				Id = responseModel.Id,
				ChildId = responseModel.ChildId,
				QuestionnaireId = responseModel.QuestionnaireId,
				Value = responseModel.Value,
				Date = responseModel.Date
			};
			
			return response;
		}

		public static ResponseModel Map(Response response)
		{
			ResponseModel responseModel = new()
			{
				Id = response.Id,
				ChildId = response.ChildId,
				QuestionnaireId = response.QuestionnaireId,
				Value = response.Value,
				Date = response.Date
			};

			return responseModel;
		}

		public static Response[] Map(ResponseModel[] responseModels)
		{
			Response[] responses = new Response[responseModels.Length];

			for (int i = 0; i < responseModels.Length; i++)
			{
				responses[i] = Map(responseModels[i]);
			}

			return responses;
		}

		public static ResponseModel[] Map(Response[] responses)
		{
			ResponseModel[] responseModels = new ResponseModel[responses.Length];

			for (int i = 0; i < responses.Length; i++)
			{
				responseModels[i] = Map(responses[i]);
			}

			return responseModels;
		}
	}
}

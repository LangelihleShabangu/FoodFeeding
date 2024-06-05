using FoodFeeding.Models;
using FoodFeeding.UI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

         /* 
			Name : Langelihle Shabangu 
			Date : 05 June 2024
			Desciption
			This is the call that makes a GET to the API in order to pull the data required.
            Also added the try catch method to make sure we returning a proper error messages to the user.
		 */

namespace FoodFeeding.UI.Services
{
    public class BaseService : IBaseServices
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            responseModel = new APIResponse();
            this.httpClient = httpClient;
        }
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
				/* Preparation to make a call to the API */

				var client = httpClient.CreateClient("IngredientItemsClient");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                message.Method = HttpMethod.Get;
                HttpResponseMessage apiResponse = null;    
                
                apiResponse = await client.SendAsync(message); /* Calling the API */

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                try
                {
                    APIResponse APIResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if (APIResponse == null)
                    {
                        APIResponse aPIResponse = new APIResponse();
                        aPIResponse.StatusCode = System.Net.HttpStatusCode.NonAuthoritativeInformation;
                        aPIResponse.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(aPIResponse);
                        var returnObject = JsonConvert.DeserializeObject<T>(res);
                        return returnObject;
                    }

                    if (APIResponse.StatusCode == System.Net.HttpStatusCode.BadRequest || APIResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        APIResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        APIResponse.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(APIResponse);
                        var returnObject = JsonConvert.DeserializeObject<T>(res);
                        return returnObject;
                    }                   
                }
                catch(Exception ex)
                {
                    return JsonConvert.DeserializeObject<T>(apiContent);                    
                }
                return JsonConvert.DeserializeObject<T>(apiContent);
            }
            catch (Exception ex) {
                var dto = new APIResponse { ErrorMessages = new List<string> { Convert.ToString(ex.Message) }, IsSuccess = false };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}

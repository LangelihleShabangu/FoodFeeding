

namespace FoodFeeding.UI.Services.IServices
{
    public class IngredientItemsService : BaseService, IIngredientItemsService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string _IngredientItemsUrl;    

        public IngredientItemsService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory) 
        {
            _clientFactory = clientFactory;
            _IngredientItemsUrl = configuration.GetValue<string>("ServiceUrls:IngredientItemsAPI");
        }
        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new FoodFeeding.Models.APIRequest() { Url = _IngredientItemsUrl + "/API/IngredientItemsAPI" });
        }
    }
}


using FoodFeeding.Models;

namespace FoodFeeding.UI.Services.IServices
{
    public interface IBaseServices
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}

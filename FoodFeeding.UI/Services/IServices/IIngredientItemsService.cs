using System.Linq.Expressions;

namespace FoodFeeding.UI.Services.IServices
{
    public interface IIngredientItemsService
    {
        Task<T> GetAllAsync<T>();        
    }
}

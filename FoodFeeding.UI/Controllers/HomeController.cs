using FoodFeeding.Models;
using FoodFeeding.Models.ModelView;
using FoodFeeding.UI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FoodFeeding.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIngredientItemsService _IngredientItemsService;

        public HomeController(IIngredientItemsService IngredientItemsService)
        {
            _IngredientItemsService = IngredientItemsService;
        }
        public async Task<IActionResult> Index()
        {
            List<ModelView> list = new();
            var response = await _IngredientItemsService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ModelView>>(Convert.ToString(response.Results)).OrderByDescending(f => f.Reciept_Name).ToList();               
            }
            return View(list);
        }
    }
}

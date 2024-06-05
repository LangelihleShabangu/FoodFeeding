using FoodFeeding.Models;
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
            List<IngredientItems> list = new();
            var response = await _IngredientItemsService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<IngredientItems>>(Convert.ToString(response.Results));
            }
            return View(list);
        }
    }
}

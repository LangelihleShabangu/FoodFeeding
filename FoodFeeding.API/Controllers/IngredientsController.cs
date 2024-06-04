using FoodFeeding.DataAccess.Repository.IRepository;
using FoodFeeding.DataAccess.Repository.RepositoryImplementation;
using FoodFeeding.Models;
using FoodFeeding.Models.ModelView;
using IngredientItemsFeeding.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoodFeeding.API.Controllers
{
    [Route("api/IngredientItemsAPI")]
    [ApiController]
    public class IngredientItemsController : ControllerBase
    {
        private APIResponse Response;
        private readonly IIngredientItems Repository;
        private readonly IReciept Reciept;
        private readonly IRecieptItems RecieptItems;
        public IngredientItemsController(IIngredientItems _Repository, IReciept _Reciept, IRecieptItems _RecieptItems)
        {
            Repository = _Repository;
            Reciept = _Reciept;
            RecieptItems = _RecieptItems;
            Response = new APIResponse();
        }

        [HttpGet]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(Duration = 30)]
        public async Task<ActionResult<APIResponse>> GetResults()
        {
            try
            {
                List<ModelView> modelViews = new List<ModelView>(); 

                var ingredientList = new List<IngredientItems>();
                ingredientList = Repository.GetAllAsync(includeProperties: "Ingredients").Result;                

                var recieptsList = new List<Reciept>();
                recieptsList = Reciept.GetAllAsync().Result;                                    
                
                foreach (var item in recieptsList.OrderByDescending(f=>f.Feeds))
                {
                    var NewIngredientList = new List<IngredientItems>();

                    var recieptItemlist = RecieptItems.GetAllAsync(includeProperties: "Food").Result.Where(f=>f.RecieptId == item.RecieptId);

                    foreach (var recieptItem in recieptItemlist)
                    {
                        var ingredientItems = new IngredientItems();
                        if (recieptItemlist != null)
                        {
                            var ingredient = ingredientList.FirstOrDefault(f => f.FoodId == recieptItem.FoodId);
                            if (ingredient != null && ingredient.Quantity >= recieptItem.Quantity)
                            {
                                ingredientItems.FoodId = recieptItem.FoodId;
                                ingredientItems.Quantity = recieptItem.Quantity;
                            }
                            else
                            {
                                ingredientItems = new IngredientItems();
                            }
                        }

                        if (ingredientItems.Quantity > 0)
                        {
                            NewIngredientList.Add(ingredientItems);
                            modelViews.Add(new ModelView() { Feeds = item.Feeds, Food_Name = recieptItem.Food.Name, Quantity = ingredientItems.Quantity, Reciept_Name = item.Reciept_Name });
                        }
                    }

                    if(NewIngredientList.Count() == recieptItemlist.Count())
                    {
                        foreach(var itemData in ingredientList)
                        {
                            if(itemData != null)
                            {
                                var data = NewIngredientList.FirstOrDefault(f => f.FoodId == itemData.FoodId);
                                itemData.Quantity = itemData.Quantity - (data== null ? 0 : data.Quantity);  
                            }
                        }
                    }
                }

                Response.Results = modelViews;
                Response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Response;
        }
    }
}

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
		/* 
			Name : Langelihle Shabangu 
			Date : 05 June 2024
			Desciption
			Use the depandancy injection to connect to the database and pull the data that will be analysed and displayed accoringly.
			Also implement the cashing as we do not have to hit the database everytime since the data is not changing.
		 */
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
				var modelViews = new List<ModelView>();

				var ingredientList = Repository.GetAllAsync(includeProperties: "Ingredients").Result; /* Pull the ingredients item including paren */

				var recieptsList = Reciept.GetAllAsync().Result;						

				foreach (var item in recieptsList.OrderByDescending(f => f.Feeds)) /* Start with the reciept that can feed more people and loop down */
				{
					var NewIngredientList = new List<IngredientItems>();

					var recieptItemlist = RecieptItems.GetAllAsync(includeProperties: "Food").Result.Where(f => f.RecieptId == item.RecieptId);

					string Latest_Reciept_Name = string.Empty;

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
						}
						if (ingredientItems.Quantity > 0)
						{
							NewIngredientList.Add(ingredientItems);
							Latest_Reciept_Name = item.Reciept_Name;
							modelViews.Add(new ModelView() { Feeds = item.Feeds, Food_Name = recieptItem.Food.Name, Quantity = ingredientItems.Quantity, Reciept_Name = item.Reciept_Name });
						}
					}

					if (recieptItemlist != null && NewIngredientList.Count() == recieptItemlist.Count()) /* Dertemine if reciept and given ingredients matches  */
					{
						foreach (var itemData in ingredientList)
						{
							if (itemData != null)
							{
								var data = NewIngredientList.FirstOrDefault(f => f.FoodId == itemData.FoodId);
								itemData.Quantity = itemData.Quantity - (data == null ? 0 : data.Quantity);
							}
						}
					}
					else					
						modelViews.RemoveAll(f => f.Reciept_Name == Latest_Reciept_Name);   /* Remove the item that has some missing ingredients */
				}

				/* Fix the data so that is can easly be pluged back to the Interface separate according to the proper structure */

				var modelView = new List<ModelView>();
				string Reciept_Name = string.Empty;
				foreach (var item in modelViews.OrderByDescending(f => f.Reciept_Name))
				{
					if (item.Reciept_Name != Reciept_Name)
					{
						modelView.Add(new ModelView() { Reciept_Name = item.Reciept_Name, Feeds = item.Feeds });
						Reciept_Name = item.Reciept_Name;
					}
					var SingleReciept = modelView.FirstOrDefault(f => f.Reciept_Name == Reciept_Name);
					if (SingleReciept != null)
					{
						SingleReciept.ModelViewList.Add(item);
					}
				}

				Response.Results = modelView;
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

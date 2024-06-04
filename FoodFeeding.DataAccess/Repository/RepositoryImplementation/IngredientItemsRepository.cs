using FoodFeeding.DataAccess.Repository.IRepository;
using FoodFeeding.Models;
using IngredientItemsFeeding.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodFeeding.DataAccess.Repository.RepositoryImplementation
{
    public class IngredientItemsRepository : Repository<IngredientItems>, IIngredientItems
    {
        private readonly FoodFeedingdbContext db;
        public IngredientItemsRepository(FoodFeedingdbContext _db) : base(_db) { db = _db; }   
    }
}

using FoodFeeding.DataAccess.Repository.IRepository;
using FoodFeeding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodFeeding.DataAccess.Repository.RepositoryImplementation
{
    public class RecieptItemsRepository : Repository<RecieptItems>, IRecieptItems
    {
        private readonly FoodFeedingdbContext db;
        public RecieptItemsRepository(FoodFeedingdbContext _db) : base(_db) { db = _db; }   
    }
}

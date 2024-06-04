using FoodFeeding.DataAccess.Repository.IRepository;
using FoodFeeding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodFeeding.DataAccess.Repository.RepositoryImplementation
{
    public class RecieptRepository : Repository<Reciept>, IReciept
    {
        private readonly FoodFeedingdbContext db;
        public RecieptRepository(FoodFeedingdbContext _db) : base(_db) { db = _db; }   
    }
}

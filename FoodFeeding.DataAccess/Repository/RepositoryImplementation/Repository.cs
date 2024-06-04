using FoodFeeding.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodFeeding.DataAccess.Repository.RepositoryImplementation
{
    public class Repository<T>: IRepository<T> where T : class
    {
        internal DbSet<T> dbSet;
        private readonly FoodFeedingdbContext db;
        public Repository(FoodFeedingdbContext _db)
        {
            db = _db;
            this.dbSet = db.Set<T>();            
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool IsTracked = true, string? IsincludeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (!IsTracked)            
                query = query.AsTracking();
            

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrEmpty(IsincludeProperties))
            {
                foreach (var includeProp in IsincludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? IsincludeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrEmpty(IsincludeProperties))
            {
                foreach (var includeProp in IsincludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.ToListAsync();
        }
    }
}

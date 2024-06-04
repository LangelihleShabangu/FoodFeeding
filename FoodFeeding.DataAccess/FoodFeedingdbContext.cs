using FoodFeeding.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodFeeding.DataAccess
{
    public class FoodFeedingdbContext : DbContext
    {
        public FoodFeedingdbContext(DbContextOptions<FoodFeedingdbContext> options) : base(options) { }
        public DbSet<Food> Food { get; set; }
        public DbSet<IngredientItems> IngredientItems { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Reciept> Reciept { get; set; }
        public DbSet<RecieptItems> RecieptItems { get; set; }       
    }
}

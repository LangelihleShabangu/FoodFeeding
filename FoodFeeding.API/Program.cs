using FoodFeeding.DataAccess;
using FoodFeeding.DataAccess.Repository.IRepository;
using FoodFeeding.DataAccess.Repository.RepositoryImplementation;
using FoodFeeding.Models;
using IngredientItemsFeeding.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IReciept, RecieptRepository>();
builder.Services.AddScoped<IRecieptItems, RecieptItemsRepository>();
builder.Services.AddScoped<IIngredients, IngredientsRepository>();
builder.Services.AddScoped<IIngredientItems, IngredientItemsRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FoodFeedingdbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

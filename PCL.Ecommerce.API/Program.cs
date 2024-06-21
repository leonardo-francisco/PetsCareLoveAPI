using Microsoft.Extensions.Options;
using PCL.Application.Mapper;
using PCL.Application.Services.Category;
using PCL.Application.Services.Employee;
using PCL.Application.Services.Product;
using PCL.Application.Services.Service;
using PCL.Domain.Interfaces;
using PCL.Domain.Utils;
using PCL.Infrastructure.Persistence;
using PCL.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<ImageHelper>();

// Configurar MongoDbSettings e MongoDbContext
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoConnectionStrings"));
builder.Services.AddSingleton<PetCareContext>();

// Register MongoDB settings
builder.Services.AddSingleton<IMongoDBSettings>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return settings;
});


// Add Mapper
builder.Services.AddAutoMapper(typeof(ConfigurationMapping));

// Add application services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();


//Add repository
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

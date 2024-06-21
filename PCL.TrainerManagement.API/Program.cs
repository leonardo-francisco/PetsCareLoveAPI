using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using PCL.Application.Mapper;
using PCL.Application.Services.Trainer;
using PCL.Application.Services.Training;
using PCL.Application.Services.TrainingRecord;
using PCL.Application.Services.TrainingResult;
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
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<ITrainingService, TrainingService>();
builder.Services.AddScoped<ITrainingRecordService, TrainingRecordService>();
builder.Services.AddScoped<ITrainingResultService, TrainingResultService>();

//Add repository
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();
builder.Services.AddScoped<ITrainingRecordRepository, TrainingRecordRepository>();
builder.Services.AddScoped<ITrainingResultRepository, TrainingResultRepository>();

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

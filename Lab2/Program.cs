using Lab2.dal;
using Lab2.dal.Settings; // Щоб бачити клас MongoDBSettings
using Lab2.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options; // Щоб працювати з налаштуваннями
using MongoDB.Driver; // Щоб працювати з клієнтом Mongo

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Отримуємо рядок підключення з appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Реєструємо LabDbContext із SQL Server
builder.Services.AddDbContext<LabDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Конфігурація MongoDB
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

// 4. Реєстрація клієнта MongoDB
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var settings = s.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Реєстрація нашого сервісу
builder.Services.AddSingleton<StudentService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<LabDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.Run();

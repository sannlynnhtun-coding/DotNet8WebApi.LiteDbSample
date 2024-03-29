using DotNet8WebApi.LiteDbSample.Services;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(n =>
{
    var _folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LiteDb");
    Directory.CreateDirectory(_folderPath);

    var _filePath = Path.Combine(_folderPath, builder.Configuration.GetSection("DbFileName").Value!);
    return new LiteDatabase(_filePath);
});

builder.Services.AddScoped<LiteDbV2Service>();

builder.Services.AddScoped<LiteDbV3Service>();
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

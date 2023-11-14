using DungeonFlutterAPI.Data;
using DungeonFlutterAPI.Services.Implementations;
using DungeonFlutterAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IWorldGenerator, SimpleWorldGenerator>();
builder.Services.AddScoped<IGame, SimpleGame>();


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

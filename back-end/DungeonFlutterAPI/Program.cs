using DungeonFlutterAPI.DAOs.Implementations;
using DungeonFlutterAPI.DAOs.Interfaces;
using DungeonFlutterAPI.Data;
using DungeonFlutterAPI.Game.Implementations;
using DungeonFlutterAPI.Game.Interfaces;
using DungeonFlutterAPI.Services.Implementations;
using DungeonFlutterAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddScoped<IPlayerDAO, PlayerDAO>();
builder.Services.AddScoped<IHighScoreDAO, HighScoreDAO>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IWorldGenerator, MemoryWorldGenerator>();
builder.Services.AddScoped<IGame, SimpleGame>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IHighScoreService, HighScoreService>();



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

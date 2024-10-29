using Microsoft.EntityFrameworkCore;
using SeaBattleWeb.Data.Context;
using SeaBattleWeb.Data.Repository;
using SeaBattleWeb.Data.Repository.Interfaces;
using SeaBattleWeb.Server.Hubs;
using SeaBattleWeb.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddLogging();
builder.Services.AddControllers();


builder.Services.AddDbContext<AppDatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.SetIsOriginAllowed(_ => true)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });

});

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IBoardRepository, BoardRepository>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseRouting();


app.UseCors();

app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();
    

app.MapHub<GameHub>("/gameHub");
app.MapHub<LobbyHub>("/lobbyHub");


app.Run();

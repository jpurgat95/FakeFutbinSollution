using FakeFutbin.Api.Data;
using FakeFutbin.Api.Repositories;
using FakeFutbin.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI for FakeFutbinDbContext, PlayerRepository
builder.Services.AddDbContextPool<FakeFutbinDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("FakeFutbinConnection"))
);
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();


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

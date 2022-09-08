using FakeFutbin.Api.Data;
using FakeFutbin.Api.Repositories;
using FakeFutbin.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI for FakeFutbinDbContext, PlayerRepository, ScoutRepository
builder.Services.AddDbContextPool<FakeFutbinDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("FakeFutbinConnection"))
);
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IScoutRepository, ScoutRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Avoiding cors policy
app.UseCors(policy =>
policy.WithOrigins("https://localhost:7295", "http://localhost:7295")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType)
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

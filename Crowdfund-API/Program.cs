using Crowdfund.DB;
using Crowdfund_API.Services;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FundDbContext>
    (options => options.UseSqlServer("Server=tcp:crowd-fund.database.windows.net,1433;Initial Catalog=crowd-fund;Persist Security Info=False;User ID=marjus001;Password=Crowd@2022;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

builder.Services.Configure<JsonOptions>
	(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddScoped<IBackerService, BackerService>();
builder.Services.AddScoped<ICreatorService, CreatorService>();
builder.Services.AddScoped<IProjectService, ProjectService>();

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

using Microsoft.EntityFrameworkCore;
using Serilog;
using Newtonsoft.Json;
using TaskOfAlifTech.Api.Extensions;
using TaskOfAlifTech.Api.Middlewares;
using TaskOfAlifTech.Service.Helpers;
using TaskOfAlifTech.Service.Mappers;
using TasOfAlifTech.Data.DbContexts;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

// Add database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add auto mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Jwt
builder.Services.AddJwtService(builder.Configuration);

// Add custom services
builder.Services.AddCustomServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

EnvironmentHelper.WebRootPath = app.Services.GetService<IWebHostEnvironment>()?.WebRootPath;

app.UseMiddleware<AppExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

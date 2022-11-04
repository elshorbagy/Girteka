using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.FileRepository;
using Repository.SQLRepository;
using Service.CsvService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();

builder.Services.AddDbContext<SQLDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.Configure<CsvConfiguration>(builder.Configuration.GetSection("Configuration"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISQLRepository, SQLRepository>();
builder.Services.AddScoped<ICsvReaderRepository, CsvReaderRepository>();
builder.Services.AddScoped<ICsvService, CsvService>();

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

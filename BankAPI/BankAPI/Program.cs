using BankAPI.Data;
using BankAPI.Data.Repository;
using BankAPI.Models;
using BankAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IBankRepository<>), typeof(BankRepository<>));

builder.Services.AddScoped(typeof(IBankService<>), typeof(BankService<>));

builder.Services.AddScoped<IBankService<Branch>, BranchService>();
builder.Services.AddScoped<IBankRepository<Branch>, BranchRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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

app.Run();

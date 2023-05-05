using Bank.API.Data;
using Bank.API.Data.Repository;
using Bank.API.Models;
using Bank.API.Models.DTOs.AccountDTOs;
using Bank.API.Models.DTOs.AddressDTOs;
using Bank.API.Models.DTOs.BranchDTOs;
using Bank.API.Models.DTOs.CustomerDTOs;
using Bank.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBankRepository<Address>, AddressRepository>();
builder.Services.AddScoped<IBankService<CreateAddressDTO, Address, Address>, AddressService>();

builder.Services.AddScoped<IBankRepository<Branch>, BranchRepository>();
builder.Services.AddScoped<IBranchService, BranchService>();

builder.Services.AddScoped<IBankRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IBankService<CreateCustomerDTO, Customer, UpdateCustomerDTO>, CustomerService>();

builder.Services.AddScoped<IBankRepository<Account>, AccountRepository>();
builder.Services.AddScoped<IBankService<CreateAccountDTO, ReadAccountDTO, UpdateAccountDTO>, AccountService>();

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

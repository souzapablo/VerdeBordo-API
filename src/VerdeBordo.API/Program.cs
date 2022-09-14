using MediatR;
using Microsoft.EntityFrameworkCore;
using VerdeBordo.Application.Features.Orders.Queries.GetAllOrders;
using VerdeBordo.Core.Persistence.Interfaces;
using VerdeBordo.Infrastructure.Persistence;
using VerdeBordo.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("VerdeBordoCs");
builder.Services.AddDbContext<VerdeBordoDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddMediatR(typeof(GetAllOrdersQuery));

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

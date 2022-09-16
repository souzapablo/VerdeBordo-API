using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VerdeBordo.Application.Features.Orders.Commands.PostOrderCommand;
using VerdeBordo.Application.Features.Orders.Validators;
using VerdeBordo.Core.Interfaces.Messages;
using VerdeBordo.Core.Interfaces.Repositories;
using VerdeBordo.Infrastructure.Common;
using VerdeBordo.Infrastructure.Persistence;
using VerdeBordo.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("VerdeBordoCs");
builder.Services.AddDbContext<VerdeBordoDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IMessageHandler, MessageHandler>();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<PostOrderCommandValidator>();
builder.Services.AddMediatR(typeof(PostOrderCommand));
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

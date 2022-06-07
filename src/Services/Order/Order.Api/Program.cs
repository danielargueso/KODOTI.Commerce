using System.Reflection;
using Common.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Persistence.Database;
using Order.Service.Queries;
using Order.Service.Queries.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext
builder.Services.AddDbContext<OrderDbContext>(opts =>
    opts.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsHistoryTable(PersistenceSettings.DbMigrationsTableName, PersistenceSettings.DbSchemaName)
        )
    );


// Add LogSystem
builder.Logging.AddProvider(
    new SyslogLoggerProvider(
        builder.Configuration.GetValue<string>("Papertrail:Host"),
        builder.Configuration.GetValue<int>("Papertrail:Port"),
        null
    )
);

// Add Dependency Injection
builder.Services.AddMediatR(Assembly.Load("Order.Services.EventHandlers"));
builder.Services.AddTransient<IOrderQueryService, OrderQueryService>();
builder.Services.AddTransient<IOrderDetailQueryService, OrderDetailQueryService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();


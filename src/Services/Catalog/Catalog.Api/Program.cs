using System.Reflection;
using Catalog.Persistence.Database;
using Catalog.Service.Queries;
using Catalog.Service.Queries.Contracts;
using Common.Logging;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsHistoryTable(PersistenceSettings.DbMigrationsTableName, PersistenceSettings.DbSchemaName)
        )
    );

// Add Health Check
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

builder.Services.AddHealthChecksUI()
    .AddInMemoryStorage();

// Add LogSystem
builder.Logging.AddProvider(
    new SyslogLoggerProvider(
        builder.Configuration.GetValue<string>("Papertrail:Host"),
        builder.Configuration.GetValue<int>("Papertrail:Port"),
        null
    )
);

// Add Dependency Injection
builder.Services.AddMediatR(Assembly.Load("Catalog.Service.EventHandlers"));
builder.Services.AddTransient<IProductQueryService, ProductQueryService>();
builder.Services.AddTransient<IProductInStockQueryService, ProductInStockQueryService>();

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

// Add Health Check End Point
app.MapHealthChecks("/healthcheck", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI();

app.UseHealthChecksUI(options =>
{
    options.UIPath = "/healthchecks-ui";
    options.ApiPath = "/health-ui-api";
});

app.Run();


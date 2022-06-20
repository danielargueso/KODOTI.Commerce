using System.Configuration;
using System.Reflection;
using System.Text;
using Common.Logging;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Order.Persistence.Database;
using Order.Service.Proxy;
using Order.Service.Proxy.Catalog;
using Order.Service.Proxy.Catalog.Contracts;
using Order.Service.Queries;
using Order.Service.Queries.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Middleware
builder.Services.AddHttpContextAccessor();

// Add DbContext
builder.Services.AddDbContext<OrderDbContext>(opts =>
    opts.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsHistoryTable(PersistenceSettings.DbMigrationsTableName, PersistenceSettings.DbSchemaName)
        )
    );

// Add Health Check
builder.Services.AddHealthChecks()
    .AddDbContextCheck<OrderDbContext>();

builder.Services.AddHealthChecksUI()
    .AddInMemoryStorage();
 
// API Urls
builder.Services.Configure<ApiUrls>(
    opts => builder.Configuration.GetSection(ApiUrls.SectionName).Bind(opts)
    );

// Proxies
builder.Services.Configure<AzureServiceBus>(
    opts => builder.Configuration.GetSection(AzureServiceBus.SectionName).Bind(opts)
    );
//builder.Services.AddHttpClient<ICatalogProxy, CatalogHttpProxy>();
builder.Services.AddTransient<ICatalogProxy, CatalogQueueProxy>();

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

// Add Autentication
var secretKey = Encoding.ASCII.GetBytes(
    builder.Configuration.GetValue<string>("SecretKey")
    );
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.RequireHttpsMetadata = false;
        opts.SaveToken = true;
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();

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


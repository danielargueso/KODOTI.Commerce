using Api.Gateway.Proxies;
using Api.Gateway.Proxies.Catalog.Contracts;
using Api.Gateway.Proxies.Customer;
using Api.Gateway.Proxies.Customer.Contracts;
using Api.Gateway.Proxies.Order;
using Api.Gateway.Proxies.Order.Contracts;

namespace Api.Gateway.WebClient.Config;

public static class StartUpConfiguration
{
    public static IServiceCollection AddAppsettingBinding(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<ApiUrls>(opts => configuration.GetSection(ApiUrls.SectionName).Bind(opts));
        return service;
    }

    public static IServiceCollection AddProxiesRegistration(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddHttpContextAccessor();

        service.AddHttpClient<IOrderProxy, OrderProxy>();
        service.AddHttpClient<ICustomerProxy, CustomerProxy>();
        service.AddHttpClient<ICatalogProxy, CatalogProxy>();

        return service;
    }
}
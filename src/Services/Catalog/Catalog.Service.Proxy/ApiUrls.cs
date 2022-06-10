namespace Catalog.Service.Proxy;

public class ApiUrls
{
    public const string SectionName = "ApiUrls";
    public string CatalogUrl { get; set; }
}

internal sealed class EndPoint
{
    public const string Stocks = @"stocks/";
}
internal sealed class ApiVersion
{
    public const string V1 = @"v1/";
}

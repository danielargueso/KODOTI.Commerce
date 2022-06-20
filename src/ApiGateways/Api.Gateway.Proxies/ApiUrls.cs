namespace Api.Gateway.Proxies;
public class ApiUrls
{
    public const string SectionName = "ApiUrls";
    public string CatalogUrl { get; set; }
    public string IdentityUrl { get; set; }
    public string CustomerUrl { get; set; }
    public string OrderUrl { get; set; }
}

internal sealed class EndPoint
{
    // 01. Identity
    public const string Identity = @"identity/";
    public const string Roles = @"roles/";
    public const string Users = @"users/";

    // 02. Catalog
    public const string Products = @"products/";
    public const string Stocks = @"stocks/";

    // 03. Customer
    public const string Clients = @"clients/";

    // 04. Order
    public const string Orders = @"orders/";

}
internal sealed class ApiVersion
{
    public const string V1 = @"v1/";
}
internal sealed class ApiPagination
{
    internal static string GetPagination(int page, int take, string? ids)
        => string.Concat(
                $"?page={page}&take={take}",
                ids != null ? $"&ids={ids}" : string.Empty
            );
    internal static string GetPagination(int page, int take)
        => GetPagination(page, take, null);
}

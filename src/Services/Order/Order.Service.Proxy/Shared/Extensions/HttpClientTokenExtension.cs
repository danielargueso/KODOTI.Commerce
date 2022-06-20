using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Order.Service.Proxy.Shared.Extensions;

public static class HttpClientTokenExtension
{
    private const string AUTHORIZE_KEY = "Authorize";

	public static void AddBearerToken(this HttpClient client, IHttpContextAccessor context)
    {
        if (context.HttpContext.User.Identity == null)
        {
            return;
        }

        if (context.HttpContext.User.Identity.IsAuthenticated && context.HttpContext.Request.Headers.ContainsKey(AUTHORIZE_KEY))
        {
            var token = context.HttpContext.Request.Headers[AUTHORIZE_KEY].ToString();

            if (string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(AUTHORIZE_KEY, token);
            }
        }
    }
}


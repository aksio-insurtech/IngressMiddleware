// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.WebUtilities;

namespace Aksio.IngressMiddleware;

public static class OpenIDConnect
{
    public static async Task<Tokens> ExchangeCodeForAccessToken(OpenIDConnectConfig config, string code)
    {
        Globals.Logger.LogInformation("ExchangeCodeForAccessToken");

        var requestContent = new Dictionary<string, string?>
        {
            {"grant_type", "authorization_code"},
            {"code", code},
            {"client_id", config.ClientId},
            {"client_secret", config.ClientSecret},
            {"redirect_uri", config.Callback}
        };

        var url = QueryHelpers.AddQueryString(config.TokenEndpoint, requestContent);
        var formContent = new FormUrlEncodedContent(requestContent);
        var tokens = await HttpHelper.PostAsync(url, formContent);

        Globals.Logger.LogInformation($"Token result : {tokens.RootElement}");

        var accessToken = tokens.RootElement.GetProperty("access_token").GetString()!;
        var idToken = tokens.RootElement.GetProperty("id_token").GetString()!;
        return new Tokens(idToken, accessToken);
    }
}

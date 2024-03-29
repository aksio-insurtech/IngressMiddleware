// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json;

namespace Aksio.IngressMiddleware.Configuration;

#pragma warning disable MA0016

/// <summary>
/// Represents the root configuration object for the ingress middleware.
/// </summary>
public class Config
{
    /// <summary>
    /// A list of URI's to always approve if request is to "/".
    /// The 'X-Original-URI' hoster will be checked against any addresses configured here.
    /// This is initially used to approve the call to /id-porten/authorized by the Container App auth ingress (so, we approve the to request what they really want from us).
    /// </summary>
    public IEnumerable<string> AlwaysApproveUris { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the <see cref="OpenIDConnectConfig"/> configuration related to IdPorten.
    /// </summary>
    public OpenIDConnectConfig IdPorten { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="TenantsConfig"/>.
    /// </summary>
    public TenantsConfig Tenants { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="TenantResolutionConfig"/> configurations.
    /// This sets one or more resolution strategies, which are processed in order until a tenant source identifier is resolved.
    /// </summary>
    public IEnumerable<TenantResolutionConfig> TenantResolutions { get; set; } = new List<TenantResolutionConfig>();

    /// <summary>
    /// Gets or sets the URL to use for getting identity details.
    /// </summary>
    public string IdentityDetailsUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the <see cref="OAuthBearerTokensConfig"/> configuration.
    /// </summary>
    public OAuthBearerTokensConfig OAuthBearerTokens { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="ImpersonationConfig"/> configuration.
    /// </summary>
    public ImpersonationConfig Impersonation { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="MutualTLSConfig"/> configuration.
    /// </summary>
    public MutualTLSConfig MutualTLS { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="AuthorizationAudienceConfig"/> configuration.
    /// The key is the aud[ience] claim, a.k.a. the clientId used for the authentication.
    /// </summary>
    public Dictionary<string, AuthorizationAudienceConfig> Authorization { get; set; } = new();

    /// <summary>
    /// Loads the configuration from the file system.
    /// </summary>
    /// <returns>A new <see cref="Config"/> instance.</returns>
    public static Config Load()
    {
        const string configFile = "./config/config.json";
        var config = new Config();
        if (File.Exists(configFile))
        {
            var configJson = File.ReadAllText(configFile);
            config = JsonSerializer.Deserialize<Config>(configJson, Globals.JsonSerializerOptions)!;
        }

        return config;
    }
}

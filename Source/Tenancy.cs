// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Aksio.Cratis.Execution;

namespace Aksio.IngressMiddleware;

public static class Tenancy
{
    readonly static Dictionary<TenantSourceIdentifierResolverType, ITenantSourceIdentifierResolver> _resolvers = new()
    {
        { TenantSourceIdentifierResolverType.Claim, new ClaimsSourceIdentifierResolver() },
        { TenantSourceIdentifierResolverType.Route, new RouteSourceIdentifierResolver() }
    };

    public static async Task<TenantId> HandleRequest(Config config, HttpRequest request, HttpResponse response)
    {
        var tenantId = string.Empty;
        var sourceIdentifier = string.Empty;
        if (_resolvers.ContainsKey(config.TenantResolution.Strategy))
        {
            sourceIdentifier = await _resolvers[config.TenantResolution.Strategy].Resolve(config, request);
        }

        if (!string.IsNullOrEmpty(sourceIdentifier))
        {
            var tenant = config.Tenants.FirstOrDefault(_ => _.Value.SourceIdentifiers.Any(t => t == sourceIdentifier));
            tenantId = tenant.Key;
            Globals.Logger.LogInformation($"Setting tenant id to '{tenant.Key}' based on source identifier ({sourceIdentifier}) resolved using {config.TenantResolution.Strategy}");
        }

        if (string.IsNullOrEmpty(tenantId))
        {
            var tenant = config.Tenants.FirstOrDefault(_ => _.Value.Domain.Equals(request.Host.Host));
            tenantId = tenant.Key;
            Globals.Logger.LogInformation($"Setting tenant id to '{tenant.Key}' based on configured host ({request.Host.Host})");
        }
        response.Headers[Headers.TenantId] = tenantId;
        return string.IsNullOrEmpty(tenantId) ? TenantId.NotSet : new TenantId(Guid.Parse(tenantId));
    }
}

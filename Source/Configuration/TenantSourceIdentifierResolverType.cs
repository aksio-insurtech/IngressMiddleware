// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.IngressMiddleware.Configuration;

/// <summary>
/// Represents the configuration for a tenant.
/// </summary>
public enum TenantSourceIdentifierResolverType
{
    /// <summary>
    /// Unconfigured.
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// No strategy is used.
    /// </summary>
    None = 1,

    /// <summary>
    /// The tenant identifier is resolved from the route.
    /// </summary>
    Route = 2,

    /// <summary>
    /// The tenant identifier is resolved from a claim.
    /// </summary>
    Claim = 3,

    /// <summary>
    /// The tenant identifier is resolved to a specific single tenant.
    /// </summary>
    Specified = 4,

    /// <summary>
    /// Resolve tenant identifier with request hostname.
    /// </summary>
    Host = 5
}
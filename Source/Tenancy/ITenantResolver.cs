// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Aksio.Execution;

namespace Aksio.IngressMiddleware.Tenancy;

/// <summary>
/// Defines a tenant resolver.
/// </summary>
public interface ITenantResolver
{
    /// <summary>
    /// Check if the resolver can resolve the tenant from the given <see cref="HttpRequest"/>.
    /// </summary>
    /// <param name="request"><see cref="HttpRequest"/> to resolve from.</param>
    /// <returns>True if it can resolve it, false if not.</returns>
    Task<bool> CanResolve(HttpRequest request);

    /// <summary>
    /// Resolve the tenant from the given <see cref="HttpRequest"/>.
    /// </summary>
    /// <param name="request"><see cref="HttpRequest"/> to resolve from.</param>
    /// <returns>Resolved <see cref="TenantId"/>.</returns>
    Task<TenantId> Resolve(HttpRequest request);
}

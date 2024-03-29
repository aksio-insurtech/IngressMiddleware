// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.IngressMiddleware.MutualTLS;

/// <summary>
/// Defines a system that can handle mTLS/Mutual TLS, a.k.a. client certificate requests.
/// </summary>
public interface IMutualTLS
{
    /// <summary>
    /// Checks if Mutual TLS is configured, and thus should be called.
    /// </summary>
    /// <returns>True if configured.</returns>
    bool IsEnabled();

    /// <summary>
    /// Handle the client certificate.
    /// </summary>
    /// <param name="request"><see cref="HttpRequest"/>.</param>
    /// <returns>OkResult() on success, or StatusCodeResult(StatusCodes.Status401Unauthorized) if not authenticated.</returns>
    IActionResult Handle(HttpRequest request);
}

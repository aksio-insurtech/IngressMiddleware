// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net;
using Aksio.IngressMiddleware.integrationtests.role_authorization.given;
using Aksio.IngressMiddleware.RoleAuthorization;
using MELT;
using Microsoft.Extensions.Logging;

namespace Aksio.IngressMiddleware.integrationtests.role_authorization.scoped_tenantresolution;

public class request_without_audience : factory_with_role_auth_with_scoped_tenancyresolution
{
    HttpResponseMessage _responseMessage;
    List<LogEntry> _logEntries;

    async Task Because()
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/");
        BuildAndSetPrincipalWithTenantClaim(requestMessage, IngressConfig.Tenants.Values.Last().SourceIdentifiers.Last(), string.Empty);

        _responseMessage = await IngressClient.SendAsync(requestMessage);

        _logEntries = IngressFactory.TestLoggerSink.LogEntries
            .Where(l => l.LoggerName == typeof(RoleAuthorizer).FullName)
            .ToList();
    }

    [Fact]
    void should_be_unauthorized() => _responseMessage.StatusCode.ShouldEqual(HttpStatusCode.Unauthorized);

    [Fact]
    void the_attempt_was_logged() => _logEntries.ShouldContain(l => l.EventId == 4 && l.LogLevel == LogLevel.Warning);
}
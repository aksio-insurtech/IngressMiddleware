// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net;
using Aksio.IngressMiddleware.integrationtests.role_authorization.given;
using Aksio.IngressMiddleware.RoleAuthorization;
using MELT;
using Microsoft.Extensions.Logging;

namespace Aksio.IngressMiddleware.integrationtests.role_authorization.scoped_tenantresolution;

public class request_with_host_resolution_and_invalid_entraid_tenant :
    factory_with_host_resolution_and_role_auth_with_scoped_tenancyresolution
{
    HttpResponseMessage _responseMessage;
    List<LogEntry> _logEntries;

    async Task Because()
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://host0002/");
        BuildAndSetPrincipalWithTenantClaim(
            requestMessage,
            EntraId1,
            AudienceWithRoles,
            "user@entraid1.com",
            AcceptedRolesPrAudience[AudienceWithRoles].Roles.ToArray());

        _responseMessage = await IngressClient.SendAsync(requestMessage);

        _logEntries = IngressFactory.TestLoggerSink.LogEntries.Where(l => l.LoggerName == typeof(RoleAuthorizer).FullName)
            .ToList();
    }

    [Fact]
    void should_be_unauthorized() => _responseMessage.StatusCode.ShouldEqual(HttpStatusCode.Forbidden);

    [Fact]
    void the_attempt_was_logged() => _logEntries.ShouldContain(l => l.EventId == 6 && l.LogLevel == LogLevel.Warning);
}
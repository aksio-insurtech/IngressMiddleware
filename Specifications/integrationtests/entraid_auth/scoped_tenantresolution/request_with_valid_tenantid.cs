// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Aksio.IngressMiddleware.integrationtests.entraid_auth.given;

namespace Aksio.IngressMiddleware.integrationtests.entraid_auth.scoped_tenantresolution;

public class request_with_valid_tenantid : factory_with_entraid_auth_with_scoped_tenancyresolution
{
    HttpResponseMessage _responseMessage;

    async Task Because()
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/");
        BuildAndSetPrincipalWithTenantClaim(
            requestMessage,
            IngressConfig.Tenants.Values.Last().SourceIdentifiers.Last(),
            AcceptedRoles.First());

        _responseMessage = await IngressClient.SendAsync(requestMessage);
    }

    [Fact]
    void access_granted() => _responseMessage.IsSuccessStatusCode.ShouldBeTrue();
}
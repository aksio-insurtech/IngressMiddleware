// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.IngressMiddleware.Impersonation.for_GroupsImpersonationAuthorizer.when_asking_if_authorized;

public class and_groups_are_configured_but_user_does_not_have_them : given.config_with_two_groups
{
    GroupsImpersonationAuthorizer _authorizer;
    bool _result;

    void Establish() => _authorizer = new(Config);

    async Task Because() => _result = await _authorizer.IsAuthorized(HttpContext.Request, ClientPrincipal.Empty);

    [Fact]
    void should_not_be_authorized() => _result.ShouldBeFalse();
}
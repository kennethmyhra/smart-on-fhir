// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Linq;

namespace SmartDemo.AuthServer
{
    public static class Config
    {
        private static readonly IEnumerable<LaunchContext> _launchContext = new LaunchContext[]
            {
                new LaunchContext
                {
                    Id = "18d79345-0864-49b2-8269-160d5101366a",
                    ClientId = "client",
                    Patient = "smart-1482713",
                },

                new LaunchContext
                {
                    Id = "21984d1d-8c96-4f76-afe4-9e9a262ccd8c",
                    ClientId = "7d26357a-6904-4b93-8759-b53f635a6241",
                    Patient = "smart-1482713",
                },

                new LaunchContext
                {
                    Id = "0a25036c-ed4c-4fad-a806-3dec4d58bc42",
                    ClientId = "growth_chart",
                    Patient = "smart-1482713",
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1", "My API"),
                new ApiScope("patient/*.read", "Read Access To Patient Data"),
                new ApiScope("launch", "Launch Smart Application"),
            };

        public static IEnumerable<Client> Clients
        {
            get
            {
                return new Client[]
{
                new Client
                {
                    ClientId = "client",
                    // no interactive user, use the clintid/secret for authentication
                    AllowedGrantTypes = GrantTypes.Code,
                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // scopes that client has access to
                    AllowedScopes = { "api1" },
                    RedirectUris = { "http://localhost:1337/index.html" },
                    RequirePkce = false,
                },
                new Client
                {
                    ClientName = "Pediatric Growth Chart Application",
                    ClientId = "growth_chart",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "patient/*.read",
                        "launch"
                    },

                    // Example of an assembled redirect url for the Growth Chart Application: 
                    // http://examples.smarthealthit.org/growth-chart-app/#state=b7ad45cf-4472-3bf4-daa2-ec5b7be29782
                    RedirectUris = { "http://localhost:56364" },
                    RequirePkce = false,
                },
};
            }
        }

        public static IEnumerable<TestUser> Users =>
            new TestUser[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password"
                }
            };

        public static IEnumerable<LaunchContext> LaunchContexts => _launchContext;

        public static LaunchContext GetLaunchContext(string launchContextId, string clientId)
        {
            return LaunchContexts
                ?.SingleOrDefault(c => c.Id == launchContextId && c.ClientId == clientId);
        }

        public static LaunchContext GetLaunchContextByCode(string code, string clientId)
        {
            return LaunchContexts
                ?.SingleOrDefault(c => c.Code == code && c.ClientId == clientId);
        }
    }
}
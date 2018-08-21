using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EHR.AuthorizationServer
{
    public class Config
    {
        public static IList<LaunchContext> GetLaunchContexts()
        {
            return new List<LaunchContext>
            {
                new LaunchContext
                {
                    Id = "21984d1d-8c96-4f76-afe4-9e9a262ccd8c",
                    ClientId = "7d26357a-6904-4b93-8759-b53f635a6241",
                    Patient = "SMART-1482713"
                }
            };
        }

        public static LaunchContext GetLaunchContext(string launchContextId, string clientId)
        {
            return GetLaunchContexts()
                ?.SingleOrDefault(c => c.Id == launchContextId && c.ClientId == clientId);
        }

        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    // No interactive user, use client_id/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // Secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // Scopes that client has access to
                    AllowedScopes = { "api1" },
                },

                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },

                new Client
                {
                    ClientName = "Pediatric Growth Chart Application",
                    ClientId = "0070e04c-8b09-4ddd-90d0-07d92ff1e000",
                    //ClientSecrets =
                    //{
                    //    new Secret("secret".Sha256()),
                    //},
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { "api1" },

                    // Example of a concrete redirect url: https://sb-apps.smarthealthit.org/apps/growth-chart/#state=b7ad45cf-4472-3bf4-daa2-ec5b7be29782
                    // State will probably just be used in the scenarios where we do not use a launch-token, 
                    // similar to scenarios where an EPJ is not involed and the application is running stand-alone
                    RedirectUris = { "https://sb-apps.smarthealthit.org/apps/growth-chart/" }
                },

                new Client
                {
                    ClientName = "Local Test Client",
                    ClientId = "7d26357a-6904-4b93-8759-b53f635a6241",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "http://localhost:5002/Launch" },

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },

                    AllowOfflineAccess = true,

                    Properties =
                    {
                        { "21984D1D-8C96-4F76-AFE4-9E9A262CCD8C", "{\"PatientId\":\"SMART-1482713\"}" }
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
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
        }
    }
}

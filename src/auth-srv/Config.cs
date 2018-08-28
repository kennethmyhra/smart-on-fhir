using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;

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
                    Patient = "smart-1482713"
                },

                new LaunchContext
                {
                    Id = "0a25036c-ed4c-4fad-a806-3dec4d58bc42",
                    ClientId = "growth_chart",
                    Patient = "smart-1482713"
                }
            };
        }

        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API"),
                new ApiResource("patient/*.read", "Read Access To Patient Data"),
                new ApiResource("launch", "Launch Smart Application")
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
                    RedirectUris = { "http://examples.smarthealthit.org/growth-chart-app/" }
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

                    AllowedGrantTypes = GrantTypes.Code,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1",
                        "patient/*.read"
                    },

                    AllowOfflineAccess = true
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

using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHR.AuthorizationServer
{
    public class CustomTokenGenerator : TokenResponseGenerator
    {
        public CustomTokenGenerator(ISystemClock clock, ITokenService tokenService, IRefreshTokenService refreshTokenService, IResourceStore resources, IClientStore clients, ILogger<TokenResponseGenerator> logger) : base(clock, tokenService, refreshTokenService, resources, clients, logger)
        {
        }

        public override Task<TokenResponse> ProcessAsync(TokenRequestValidationResult request)
        {
            request.CustomResponse = new Dictionary<string, object>
            {
                { "patient", "SMART-1482713" }
            };

            return base.ProcessAsync(request); 
        }
    }
}

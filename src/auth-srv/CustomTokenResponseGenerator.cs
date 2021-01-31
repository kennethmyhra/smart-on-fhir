using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDemo.AuthServer
{
    public class CustomTokenResponseGenerator : TokenResponseGenerator
    {
        public CustomTokenResponseGenerator(ISystemClock clock, ITokenService tokenService, IRefreshTokenService refreshTokenService, IScopeParser scopeParser, IResourceStore resources, IClientStore clients, ILogger<TokenResponseGenerator> logger) 
            : base(clock, tokenService, refreshTokenService, scopeParser, resources, clients, logger)
        {
        }

        public override async Task<TokenResponse> ProcessAsync(TokenRequestValidationResult request)
        {
            var clientId = request.ValidatedRequest.Client.ClientId;
            var code = request.ValidatedRequest.Raw.Get("code");
            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(code))
            {
                LaunchContext launchContext = Config.GetLaunchContextByCode(code, clientId);
                if (launchContext != null)
                {
                    request.CustomResponse = new Dictionary<string, object>
                    {
                        { "patient", launchContext.Patient },
                        { "practitioner", launchContext.Practitioner },
                        { "encounter", launchContext.Encounter }
                    };
                }
            }

            return await base.ProcessAsync(request);
        }
    }
}

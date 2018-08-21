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
            string clientId = request.ValidatedRequest.Client.ClientId;
            string launchId = request.ValidatedRequest.Raw.Get("launch");

            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(launchId))
            {
                LaunchContext launchContext = Config.GetLaunchContext(launchId, clientId);
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

            return base.ProcessAsync(request); 
        }
    }
}

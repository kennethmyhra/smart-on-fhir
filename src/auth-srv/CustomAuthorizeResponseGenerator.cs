using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SmartDemo.AuthServer
{
    public class CustomAuthorizeResponseGenerator : AuthorizeResponseGenerator
    {
        public CustomAuthorizeResponseGenerator(ISystemClock clock, ITokenService tokenService, IKeyMaterialService keyMaterialService, IAuthorizationCodeStore authorizationCodeStore, ILogger<AuthorizeResponseGenerator> logger, IEventService events) 
            : base(clock, tokenService, keyMaterialService, authorizationCodeStore, logger, events)
        {
        }

        public override async Task<AuthorizeResponse> CreateResponseAsync(ValidatedAuthorizeRequest request)
        {
            var response = await base.CreateResponseAsync(request);

            var clientId = request.ClientId;
            var launchId = request.Raw.Get("launch");
            var code = response.Code;
            if(!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(launchId))
            {
                var launchContext = Config.GetLaunchContext(launchId, clientId);
                if(launchContext != null)
                {
                    launchContext.Code = code;
                }
            }

            return response;
        }
    }
}

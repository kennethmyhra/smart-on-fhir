using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHR.AuthorizationServer
{
    public class CustomClientSecretValidator : ISecretValidator
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomClientSecretValidator"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public CustomClientSecretValidator(ILogger<CustomClientSecretValidator> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Validates a secret
        /// </summary>
        /// <param name="secrets">The stored secrets.</param>
        /// <param name="parsedSecret">The received secret.</param>
        /// <returns>
        /// A validation result
        /// </returns>
        /// <exception cref="System.ArgumentException">id or credential is missing.</exception>
        public Task<SecretValidationResult> ValidateAsync(IEnumerable<Secret> secrets, ParsedSecret parsedSecret)
        {
            // Currently does no validation just returns Success.
            return Task.FromResult(new SecretValidationResult { Success = true });
        }

        
    }
}

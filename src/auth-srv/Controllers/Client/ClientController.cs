using System.Threading.Tasks;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;

namespace auth_srv.Controllers.Client
{
    [Route("api/client")]
    //[Authorize(AuthenticationSchemes = "token")]
    public class ClientController : ControllerBase
    {
        private readonly IClientStore _clientStore;

        public ClientController(IClientStore clientStore)
        {
            _clientStore = clientStore;
        }

        [Route("{clientId}")]
        public async Task<IActionResult> Get(string clientId)
        {
            IdentityServer4.Models.Client client = await _clientStore.FindClientByIdAsync(clientId);
            return Ok(client);
        }
    }
}
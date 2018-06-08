using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;

namespace auth_srv.Controllers.ClientProperty
{
    [Route("api/client/property")]
    //[Authorize(AuthenticationSchemes = "token")]
    public class ClientPropertyController : Controller
    {
        private readonly IConfigurationDbContext _configurationDbContext;
        private readonly IClientStore _clientStore;

        public ClientPropertyController(IConfigurationDbContext configurationDbContext, IClientStore clientStore)
        {
            _configurationDbContext = configurationDbContext;
            _clientStore = clientStore;
        }

        [Route("{clientId}")]
        public async Task<IActionResult> Get(string clientId)
        {
            IdentityServer4.Models.Client client = await _clientStore.FindClientByIdAsync(clientId);
            if (client == null) return NotFound();

            return Ok(client.Properties);
        }

        [HttpPost]
        [Route("{clientId}")]
        public async Task<IActionResult> Post(string clientId, string key, string value)
        {
            //IdentityServer4.Models.Client client = await _clientStore.FindClientByIdAsync(clientId);
            IdentityServer4.EntityFramework.Entities.Client client = _configurationDbContext.Clients.SingleOrDefault(c => c.ClientId == clientId);
            if (client == null) return NotFound();

            IdentityServer4.EntityFramework.Entities.ClientProperty clientProperty = null;
            if (client.Properties == null)
            {
                client.Properties = new List<IdentityServer4.EntityFramework.Entities.ClientProperty>();
            }
            else
            {
                clientProperty = client.Properties?.SingleOrDefault(prop => prop.Key == key);
            }
            if (clientProperty != null)
            //if (client.Properties.ContainsKey(key))
            {
                //client.Properties.Remove(key);
                client.Properties.Remove(clientProperty);
            }
            //client.Properties.Add(key, value);
            client.Properties.Add(new IdentityServer4.EntityFramework.Entities.ClientProperty { Client = client, Key = key, Value = value });
            //client.Properties.Add(new KeyValuePair<string, string>(key, value));

            //_configurationDbContext.Clients.Attach(client.ToEntity());
            //_configurationDbContext.Clients.Update(client.ToEntity());
            await _configurationDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MagnetSearcher.Models;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;

namespace MagnetSearcher.Controllers {
    [Route("v1/[controller]")]
    [ApiController]
    public class DHTController : Controller {
        private IBus Bus { get; set; }
        public DHTController(IBus bus) {
            this.Bus = bus;
        }
        [HttpPost("add/{token}")]
        public async Task<IActionResult> AddMagnet(string token, [FromBody] MagnetInfo info) {
            if (token.Equals(Env.RootToken) || Env.RootToken.Equals(string.Empty)) {
                await Bus.PubSub.PublishAsync(info);
                return Ok(info);
            } else {
                return Forbid();
            }
        }
    }
}

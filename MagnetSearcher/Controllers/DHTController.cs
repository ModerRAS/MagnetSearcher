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
        public async Task<IActionResult> AddMagnet(string token, [FromBody] MagnetInfoJson infoJson) {
            if (token.Equals(Env.RootToken) || Env.RootToken.Equals(string.Empty)) {
                var info = new MagnetInfo() {
                    Files = new List<MagnetFile>(),
                    GetDateTime = infoJson.GetDateTime,
                    InfoHash = infoJson.InfoHash,
                    Length = infoJson.Length,
                    Name = infoJson.Name,
                    RawMetaDataBase64 = infoJson.RawMetaDataBase64
                };
                await Bus.PubSub.PublishAsync(info);
                return Ok();
            } else {
                return Forbid();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SSAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MagnetSearcher.Controllers {
    [Route("v1/[controller]")]
    public class DHTController : Controller {
        [HttpPost("add/{token}")]
        public async Task<IActionResult> AddMagnet(string token, [FromBody] MagnetInfo info) {
            var db = Env.DHTDatabase;
            var magnetInfo = db.GetCollection<MagnetInfo>("MagnetInfo");

            var MagnetIsExists = magnetInfo.Find(magnet => magnet.InfoHash.Equals(info.InfoHash));
            if (!MagnetIsExists.Any()) {
                magnetInfo.Insert(info);
                magnetInfo.EnsureIndex(x => x.InfoHash);
                return Ok("Inserted");
            } else {
                return Ok("Exists");
            }
        }
    }
}

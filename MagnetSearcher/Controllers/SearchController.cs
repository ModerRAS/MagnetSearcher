using Microsoft.AspNetCore.Mvc;

namespace MagnetSearcher.Controllers {
    public class SearchController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}

using EasyNetQ;
using MagnetSearcher.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagnetSearcher.Controllers {
    public class SearchController : Controller {
        private IBus Bus { get; set; }
        public SearchController(IBus bus) {
            this.Bus = bus;
        }
        public async Task<IActionResult> Index(string KeyWord, int Skip, int Take) {
            var option = await TestSearch(KeyWord, Skip, Take);
            return View(option);
        }
        public async Task<ResponseSearchOption> Search(string KeyWord, int Skip, int Take) {
            var option = await Bus.Rpc.RequestAsync<RequestSearchOption, ResponseSearchOption>(new RequestSearchOption() {
                KeyWord = KeyWord,
                Id = Guid.NewGuid(),
                Skip = Skip,
                Take = Take
            });
            return option;
        }
        public async Task<ResponseSearchOption> TestSearch(string KeyWord, int Skip, int Take) {
            return new ResponseSearchOption() {
                Count = 2,
                Id = Guid.NewGuid(),
                Skip = 0,
                Take = 20,
                Info = new List<MagnetInfo>() {
                    new MagnetInfo() {
                        InfoHash = "111",
                        Name = "test",
                        Length = 200,
                        GetDateTime = DateTime.Now.Ticks
                    },
                    new MagnetInfo() {
                        InfoHash = "121",
                        Name = "test2",
                        Length = 202,
                        GetDateTime = DateTime.Now.Ticks
                    },
                }
            };
        }
    }
}

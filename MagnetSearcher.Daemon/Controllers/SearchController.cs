using EasyNetQ;
using MagnetSearcher.Daemon.Interfaces;
using MagnetSearcher.Daemon.Services;
using MagnetSearcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Daemon.Controllers {
    public class SearchController : IController {
        private IBus Bus { get; set; }
        private SearchService searchService { get; set; }
        public SearchController(IBus bus, SearchService searchService) { 
            Bus = bus;
        }
        public async Task ExecAsync() {
            await Bus.Rpc.RespondAsync<RequestSearchOption, ResponseSearchOption>(searchService.ExecAsync);
        }
    }
}

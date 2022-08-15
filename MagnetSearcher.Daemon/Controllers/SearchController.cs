using EasyNetQ;
using MagnetSearcher.Daemon.Interfaces;
using MagnetSearcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Daemon.Controllers {
    public class SearchController : IController {
        public async Task ExecAsync() {
            var bus = RabbitHutch.CreateBus("host=localhost");
            bus.Rpc.RequestAsync<RequestSearchOption, ResponseSearchOption>();

        }
    }
}

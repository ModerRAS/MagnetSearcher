﻿using EasyNetQ;
using MagnetSearcher.Daemon.Interfaces;
using MagnetSearcher.Daemon.Services;
using MagnetSearcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Daemon.Controllers {
    public class MagnetController : IController {
        private IBus Bus { get; set; }
        private MagnetService magnetService { get; set; }
        public MagnetController(IBus bus, MagnetService magnetService) {
            this.Bus = bus;
            this.magnetService = magnetService;
        }
        public async Task ExecAsync() {
            await Bus.PubSub.SubscribeAsync<MagnetInfo>("MagnetService", magnetService.ExecAsync);
        }
    }
}

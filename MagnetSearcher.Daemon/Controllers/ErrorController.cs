using EasyNetQ;
using EasyNetQ.SystemMessages;
using Queue = EasyNetQ.Topology.Queue;
using MagnetSearcher.Daemon.Interfaces;
using MagnetSearcher.Daemon.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Daemon.Controllers {
    public class ErrorController : IController {
        private IBus Bus { get; set; }
        private ILogger Logger { get; set; }
        private const string QueueName = "EasyNetQ_Default_Error_Queue";
        public ErrorController(IBus bus, ILogger<ErrorController> logger) {
            Bus = bus;
            Logger = logger;
        }
        public async Task ExecAsync() {

            var queue = new Queue(QueueName, false);

            Action<IMessage<Error>, MessageReceivedInfo> foo = (error, info) => {
                Logger.Log(LogLevel.Warning, error.Body.Exchange);
                Logger.Log(LogLevel.Warning, error.Body.Queue);
                Logger.Log(LogLevel.Warning, error.Body.Exception);
                Logger.Log(LogLevel.Warning, error.Body.Message);
                Logger.Log(LogLevel.Warning, error.Body.DateTime.ToString());
            };

            Bus.Advanced.Consume<Error>(queue, foo);
        }
    }
}

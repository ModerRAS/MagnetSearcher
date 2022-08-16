using MagnerSearcher.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EasyNetQ.DI.Microsoft;
using EasyNetQ.DI;

namespace MagnetSearcher.Daemon {
    public static class Program {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(service => {
                    service.AddSingleton<ILoggerFactory, LoggerFactory>();
                    service.AddSingleton<LuceneManager>();
                    service.RegisterEasyNetQ(Env.EasyNetQConnectiongString);
                });
        public static void Main(string[] args) {
            IHost host = CreateHostBuilder(args)
                .ConfigureLogging(logging =>
                logging.AddFilter("System", LogLevel.Warning)
                  .AddFilter("Microsoft", LogLevel.Warning))
                .Build();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EasyNetQ.DI.Microsoft;
using EasyNetQ.DI;
using MagnetSearcher.Daemon.Interfaces;
using MagnetSearcher.Daemon.Services;
using MagnetSearcher.Daemon.Managers;
using MagnetSearcher.Models;

namespace MagnetSearcher.Daemon {
    public static class Program {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(service => {
                    service.AddSingleton<ILoggerFactory, LoggerFactory>();
                    service.AddSingleton<ISearchManager<MagnetInfo>, MeiliManager>();
                    service.AddScoped<MagnetService>();
                    service.AddScoped<SearchService>();
                    service.RegisterEasyNetQ(Env.EasyNetQConnectiongString);
                    service.Scan(scan => scan
                        .FromAssemblyOf<IController>()
                        .AddClasses(classes => classes.AssignableTo<IController>())
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    );
                }).ConfigureLogging((logging) => {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                });
        public static void Main(string[] args) {
            IHost host = CreateHostBuilder(args)
                .ConfigureLogging(logging =>
                logging.AddFilter("System", LogLevel.Warning)
                  .AddFilter("Microsoft", LogLevel.Warning))
                .Build();
            var tasks = new List<Task>();
            foreach (var e in host.Services.GetServices<IController>()) {
                tasks.Add(e.ExecAsync());
            }
            host.Run();
            Task.WaitAll(tasks.ToArray());
        }
    }
}
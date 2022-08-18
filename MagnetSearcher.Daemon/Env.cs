using LiteDB;

namespace MagnetSearcher.Daemon {
    public class Env {
        public static readonly string BasePath = Environment.GetEnvironmentVariable("BasePath") ?? ".";
        public static readonly LiteDatabase DHTDatabase = new LiteDatabase($"{BasePath}/DHT.db");
        public static readonly string EasyNetQConnectiongString = Environment.GetEnvironmentVariable("EasyNetQConnectiongString") ?? string.Empty;
    }
}

using LiteDB;

namespace MagnetSearcher {
    public class Env {
        public static readonly string RootToken = Environment.GetEnvironmentVariable("RootToken") ?? string.Empty;
        public static readonly LiteDatabase DHTDatabase = new LiteDatabase("DHT.db");
        public static readonly string EasyNetQConnectiongString = Environment.GetEnvironmentVariable("EasyNetQConnectiongString") ?? string.Empty;
    }
}

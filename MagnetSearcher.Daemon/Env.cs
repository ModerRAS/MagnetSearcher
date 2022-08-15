using LiteDB;

namespace MagnetSearcher {
    public class Env {
        public static readonly LiteDatabase DHTDatabase = new LiteDatabase("DHT.db");
        public static readonly string EasyNetQConnectiongString = "";
    }
}

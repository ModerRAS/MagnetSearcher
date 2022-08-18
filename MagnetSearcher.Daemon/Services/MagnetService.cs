using MagnerSearcher.Managers;
using MagnetSearcher.Daemon.Interfaces;
using MagnetSearcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Daemon.Services {
    public class MagnetService : IService<MagnetInfo, bool>{
        private LuceneManager luceneManager { get; set; }
        public MagnetService(LuceneManager luceneManager) { 
            this.luceneManager = luceneManager;
        }
        public async Task PushToLiteDB(MagnetInfo data) {
            var db = Env.DHTDatabase;
            var magnetInfo = db.GetCollection<MagnetInfo>("MagnetInfo");

            var MagnetIsExists = magnetInfo.Find(magnet => magnet.InfoHash.Equals(data.InfoHash));
            if (!MagnetIsExists.Any()) {
                magnetInfo.Insert(data);
                magnetInfo.EnsureIndex(x => x.InfoHash);
            }
        }
        public async Task PushToLucene(MagnetInfo data) {
            await luceneManager.WriteDocumentAsync(data);
        }
        public async Task<bool> ExecAsync(MagnetInfo data) {
            await PushToLiteDB(data);
            await PushToLucene(data);
            return true;
        }
    }
}

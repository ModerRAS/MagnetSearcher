using MagnetSearcher.Daemon.Interfaces;
using MagnetSearcher.Daemon.Managers;
using MagnetSearcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Daemon.Services {
    public class MagnetService : IService<MagnetInfo, bool>{
        private ISearchManager<MagnetInfo> iSearchManager { get; set; }
        public MagnetService(ISearchManager<MagnetInfo> iSearchManager) { 
            this.iSearchManager = iSearchManager;
        }
        public async Task<bool> ExecAsync(MagnetInfo data) {
            var db = Env.DHTDatabase;
            var magnetInfo = db.GetCollection<MagnetInfo>("MagnetInfo");

            var MagnetIsExists = magnetInfo.Find(magnet => magnet.InfoHash.Equals(data.InfoHash));
            
            if (!MagnetIsExists.Any()) {
                magnetInfo.Insert(data);
                magnetInfo.EnsureIndex(x => x.InfoHash);
                await iSearchManager.AddAsync(data);
            } else if (string.IsNullOrEmpty(MagnetIsExists.First().RawMetaDataBase64)) {
                magnetInfo.Update(data);
            }
            return true;
        }
    }
}

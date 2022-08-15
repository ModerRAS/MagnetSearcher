using MagnetSearcher.Daemon.Interfaces;
using MagnetSearcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Daemon.Services {
    public class MagnetService : IService<MagnetInfo, bool>{
        public Task<bool> ExecAsync(MagnetInfo data) {
            var db = Env.DHTDatabase;
            var magnetInfo = db.GetCollection<MagnetInfo>("MagnetInfo");

            var MagnetIsExists = magnetInfo.Find(magnet => magnet.InfoHash.Equals(data.InfoHash));
            if (!MagnetIsExists.Any()) {
                magnetInfo.Insert(data);
                magnetInfo.EnsureIndex(x => x.InfoHash);
            }
            return Task.FromResult(true);
        }
    }
}

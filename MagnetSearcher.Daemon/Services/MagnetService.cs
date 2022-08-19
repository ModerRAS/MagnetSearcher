﻿using MagnetSearcher.Daemon.Interfaces;
using MagnetSearcher.Daemon.Managers;
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
        public async Task<bool> ExecAsync(MagnetInfo data) {
            var db = Env.DHTDatabase;
            var magnetInfo = db.GetCollection<MagnetInfo>("MagnetInfo");

            var MagnetIsExists = magnetInfo.Find(magnet => magnet.InfoHash.Equals(data.InfoHash));
            var first = MagnetIsExists.First();
            if (!MagnetIsExists.Any()) {
                magnetInfo.Insert(data);
                magnetInfo.EnsureIndex(x => x.InfoHash);
                await luceneManager.WriteDocumentAsync(data);
            } else if (string.IsNullOrEmpty(first.RawMetaDataBase64)) {
                magnetInfo.Update(data);
            }
            return true;
        }
    }
}

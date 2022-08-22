using MagnetSearcher.Daemon.Interfaces;
using MagnetSearcher.Models;
using Meilisearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Daemon.Managers {
    internal class MeiliManager : ISearchManager<MagnetInfo> {
        private MeilisearchClient client { get; set; }
        public MeiliManager() {
            client = new MeilisearchClient(Env.MeiliSearchUrl, Env.MeiliSearchMasterKey);
        }

        public void Add(MagnetInfo info) {
            throw new NotImplementedException();
        }

        public Task AddAsync(MagnetInfo info) {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<MagnetInfo> infos) {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<MagnetInfo> infos) {
            throw new NotImplementedException();
        }

        public void Search(string q, int Skip, int Take, out int Count, out List<MagnetInfo> SearchResult) {
            throw new NotImplementedException();
        }

        public Task SearchAsync(string q, int Skip, int Take, out int Count, out List<MagnetInfo> SearchResult) {
            throw new NotImplementedException();
        }
    }
}

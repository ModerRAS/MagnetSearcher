using MagnetSearcher.Daemon.Interfaces;
using MagnetSearcher.Models;
using Meilisearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Daemon.Managers {
    public class MeiliManager : ISearchManager<MagnetInfo> {
        private MeilisearchClient client { get; set; }
        public MeiliManager() {
            client = new MeilisearchClient(Env.MeiliSearchUrl, Env.MeiliSearchMasterKey);
        }

        public void Add(MagnetInfo info) {
            var index = client.Index("MagnetInfo");
            var task = index.AddDocumentsAsync(new MagnetInfo[1]{ info }).Result;
        }

        public async Task AddAsync(MagnetInfo info) {
            var index = client.Index("MagnetInfo");
            await index.AddDocumentsAsync(new MagnetInfo[1] { info });
        }

        public void AddRange(IEnumerable<MagnetInfo> infos) {
            var index = client.Index("MagnetInfo");
            _ = index.AddDocumentsAsync(infos).Result;
        }

        public async Task AddRangeAsync(IEnumerable<MagnetInfo> infos) {
            var index = client.Index("MagnetInfo");
            await index.AddDocumentsAsync(infos);
        }

        public async Task<CommonSearchResult<MagnetInfo>> SearchAsync(string q, int Skip, int Take) {
            var index = client.Index("MagnetInfo");
            var magnetInfos = await index.SearchAsync<MagnetInfo>(q, new SearchQuery() { Offset = Skip, Limit = Take});
            return new CommonSearchResult<MagnetInfo>() { Count = magnetInfos.EstimatedTotalHits, Result = magnetInfos.Hits.ToList() };
        }

        public CommonSearchResult<MagnetInfo> Search(string q, int Skip, int Take) {
            var index = client.Index("MagnetInfo");
            var magnetInfos = index.SearchAsync<MagnetInfo>(q, new SearchQuery() { Offset = Skip, Limit = Take }).Result;
            return new CommonSearchResult<MagnetInfo>() { Count = magnetInfos.EstimatedTotalHits, Result = magnetInfos.Hits.ToList() };
        }
    }
}

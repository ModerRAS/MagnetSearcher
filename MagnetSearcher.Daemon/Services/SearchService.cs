using MagnetSearcher.Daemon.Interfaces;
using MagnetSearcher.Daemon.Managers;
using MagnetSearcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Daemon.Services {
    public class SearchService : IService<RequestSearchOption, ResponseSearchOption> {
        private ISearchManager<MagnetInfo> searchManager { get; set; }
        public SearchService(ISearchManager<MagnetInfo> searchManager) {
            this.searchManager = searchManager;
        }
        public async Task<ResponseSearchOption> ExecAsync(RequestSearchOption data) {
            int total;
            List<MagnetInfo> MagnetInfos;
            var result = await searchManager.SearchAsync(data.KeyWord, data.Skip, data.Take);
            return new ResponseSearchOption() {
                Id = data.Id,
                Count = result.Count,
                KeyWord = data.KeyWord,
                Info = result.Result,
                Skip = data.Skip,
                Take = data.Take
            };
        }
    }
}

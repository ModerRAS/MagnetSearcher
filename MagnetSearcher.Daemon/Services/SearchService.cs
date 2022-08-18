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
        private LuceneManager luceneManager { get; set; }
        public SearchService(LuceneManager luceneManager) {
            this.luceneManager = luceneManager;
        }
        public async Task<ResponseSearchOption> ExecAsync(RequestSearchOption data) {
            var (total, MagnetInfos) = luceneManager.Search(data.KeyWord, data.Skip, data.Take);
            return new ResponseSearchOption() {
                Id = data.Id,
                Count = total,
                KeyWord = data.KeyWord,
                Info = MagnetInfos,
                Skip = data.Skip,
                Take = data.Take
            };
        }
    }
}

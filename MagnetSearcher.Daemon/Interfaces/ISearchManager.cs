using MagnetSearcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Daemon.Interfaces {
    public interface ISearchManager<T> {
        public void Add(T info);
        public Task AddAsync(T info);
        public void AddRange(IEnumerable<T> infos);
        public Task AddRangeAsync(IEnumerable<T> infos);
        public CommonSearchResult<T> Search(string q, int Skip, int Take);
        public Task<CommonSearchResult<T>> SearchAsync(string q, int Skip, int Take);
    }
}

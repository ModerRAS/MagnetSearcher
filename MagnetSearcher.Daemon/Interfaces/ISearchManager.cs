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
        public void Search(string q, int Skip, int Take, out int Count, out List<T> SearchResult);
        public Task SearchAsync(string q, int Skip, int Take, out int Count, out List<T> SearchResult);
    }
}

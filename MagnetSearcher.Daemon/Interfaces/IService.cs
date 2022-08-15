using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Daemon.Interfaces {
    public interface IService<TRequest, TResponse> {
        public Task<TResponse> ExecAsync(TRequest data);
    }
}

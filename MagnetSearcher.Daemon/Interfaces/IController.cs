using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Daemon.Interfaces {
    public interface IController {
        public Task ExecAsync();
    }
}

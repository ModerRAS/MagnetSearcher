﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Models {
    public class RequestSearchOption {
        public Guid Id { get; set; }
        public string KeyWord { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}

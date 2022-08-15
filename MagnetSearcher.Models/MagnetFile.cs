using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetSearcher.Models {
    public class MagnetFile {
        [JsonProperty(PropertyName = "path")]
        public List<string> Path { get; set; }
        [JsonProperty(PropertyName = "length")]
        public long Length { get; set; }
    }
}

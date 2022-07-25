using Newtonsoft.Json;
using System.Collections.Generic;

namespace SSAPI.Models {
    public class MagnetInfo {
        [JsonProperty(PropertyName = "infohash")]
        public string InfoHash { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "files")]
        public List<MagnetFile> Files { get; set; }
        [JsonProperty(PropertyName = "length")]
        public long Length { get; set; }
        [JsonProperty(PropertyName = "rawmetadatabase64")]
        public string RawMetaDataBase64 { get; set; }
        [JsonProperty(PropertyName = "getdatetime")]
        public long GetDateTime { get; set; }
    }
    public class MagnetFile {
        [JsonProperty(PropertyName = "path")]
        public List<string> Path { get; set; }
        [JsonProperty(PropertyName = "length")]
        public long Length { get; set; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace MagnetSearcher.Models {
    public class MagnetInfoJson {
        [JsonProperty(PropertyName = "infohash")]
        public string InfoHash { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "length")]
        public long Length { get; set; }
        [JsonProperty(PropertyName = "rawmetadatabase64")]
        public string RawMetaDataBase64 { get; set; }
        [JsonProperty(PropertyName = "getdatetime")]
        public long GetDateTime { get; set; }
    }
}

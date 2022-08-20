using Newtonsoft.Json;
using System.Collections.Generic;

namespace MagnetSearcher.Models {
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
        public DateTime DateTime { 
            get { 
                return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).Add(new TimeSpan(GetDateTime * 10000000)); 
            } 
        }
    }
}

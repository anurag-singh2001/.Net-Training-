using Newtonsoft.Json;

namespace CollegeMgmtSystem.Models
{
    public class DepartmentModel
    {
        [JsonProperty(PropertyName = "uid", NullValueHandling = NullValueHandling.Ignore)]

        public string Uid { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]

        public string Id { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]

        public string Name { get; set; }

        [JsonProperty(PropertyName = "clguid", NullValueHandling = NullValueHandling.Ignore)]

        public string ClgUid { get; set; }

        [JsonProperty(PropertyName = "count", NullValueHandling = NullValueHandling.Ignore)]

        public int Count { get; set; }
    }
}

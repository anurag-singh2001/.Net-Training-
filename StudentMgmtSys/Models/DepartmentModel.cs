using Newtonsoft.Json;

namespace StudentMgmtSys.Models
{
    public class DepartmentModel
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Dept { get; set; }

        [JsonProperty(PropertyName = "count", NullValueHandling = NullValueHandling.Ignore)]
        public int Count { get; set; }
    }
}

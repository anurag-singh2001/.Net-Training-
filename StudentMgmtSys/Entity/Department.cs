using Newtonsoft.Json;

namespace StudentMgmtSys.Entity
{
    public class Department : BaseEntity
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Dept { get; set; }

        [JsonProperty(PropertyName = "count", NullValueHandling = NullValueHandling.Ignore)]
        public int Count { get; set; }


    }
}

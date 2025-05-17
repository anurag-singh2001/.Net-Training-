using Newtonsoft.Json;

namespace CollegeMgmtSystem.Entity
{
    public class Department : BaseEntity
    {
       

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]

        public string Name { get; set; }

        [JsonProperty(PropertyName = "clguid", NullValueHandling = NullValueHandling.Ignore)]

        public string ClgUid { get; set; }

        [JsonProperty(PropertyName = "count", NullValueHandling = NullValueHandling.Ignore)]

        public int Count { get; set; }


    }
}

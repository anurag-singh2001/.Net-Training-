using Newtonsoft.Json;

namespace CollegeMgmtSystem.Models
{
    public class CollegeModel
    {
        [JsonProperty(PropertyName = "uid", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]

        public string Id { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "dept", NullValueHandling = NullValueHandling.Ignore)]

        public List<string> Dept { get; set; }




    }
}

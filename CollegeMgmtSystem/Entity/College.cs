using CollegeMgmtSystem.Models;
using Newtonsoft.Json;

namespace CollegeMgmtSystem.Entity
{
    public class College : BaseEntity
    {

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "dept", NullValueHandling = NullValueHandling.Ignore)]

        public List<string> Dept { get; set; }




    }
}

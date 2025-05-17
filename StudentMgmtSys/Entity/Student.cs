using Newtonsoft.Json;

namespace StudentMgmtSys.Entity
{
    public class Student : BaseEntity
    {

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "course", NullValueHandling = NullValueHandling.Ignore)]

        public string Course { get; set; }

        [JsonProperty(PropertyName = "rollNo", NullValueHandling = NullValueHandling.Ignore)]
        public int RollNo { get; set; }
    }
}

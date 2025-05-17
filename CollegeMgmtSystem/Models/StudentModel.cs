using Newtonsoft.Json;

namespace CollegeMgmtSystem.Models
{
    public class StudentModel
    {
        [JsonProperty(PropertyName = "uid", NullValueHandling = NullValueHandling.Ignore)]
        public string? UId { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]

        public string? Id { get; set; }

        [JsonProperty(PropertyName = "deptuid", NullValueHandling = NullValueHandling.Ignore)]

        public string DeptUid { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]

        public string Email { get; set; }

        [JsonProperty(PropertyName = "clgname", NullValueHandling = NullValueHandling.Ignore)]

        public string? ClgName { get; set; }

        [JsonProperty(PropertyName = "deptname", NullValueHandling = NullValueHandling.Ignore)]

        public string? DeptName { get; set; }

        [JsonIgnore]
        public IFormFile? Photo { get; set; } 

        [JsonProperty(PropertyName = "photopath", NullValueHandling = NullValueHandling.Ignore)]
        public string? PhotoPath { get; set; } 
    }
}

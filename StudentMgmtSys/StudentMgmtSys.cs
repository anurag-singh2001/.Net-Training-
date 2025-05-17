using Newtonsoft.Json;
using StudentMgmtSys.Controllers;
using StudentMgmtSys.Models;

namespace StudentMgmtSys
{

    public class StudentMgmtSys
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public String name { get; set; }
        [JsonProperty("rollno")]
        public int rollno { get; set; }
        [JsonProperty("marks")]
        public int marks { get; set; }

        public static List<StudentModel> getAll()
        {
            List<StudentModel> students = new List<StudentModel>();

            return students;
        }






    }
}

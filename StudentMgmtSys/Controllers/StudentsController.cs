using System.Collections.Concurrent;
using System.Security.Policy;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using StudentMgmtSys.Entity;
using StudentMgmtSys.Models;

namespace StudentMgmtSys.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private Container _container;
        private CosmosClient _cosmosClient;


        public StudentsController(CosmosClient client)
        {
            _cosmosClient = new CosmosClient(Environment.GetEnvironmentVariable("cosmos-url"), Environment.GetEnvironmentVariable("auth-token"));
            _container = _cosmosClient.GetContainer(Environment.GetEnvironmentVariable("database-name"), Environment.GetEnvironmentVariable("container-name"));
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentModel studentModel)
        {
            //Map data model into enitity
            var student = new Student();
            student.Name = studentModel.Name;
            student.Email = studentModel.Email;
            student.Course = studentModel.Course;
            student.RollNo = studentModel.RollNo;

            student.Initialize(true, "student", "Anurag", "Anurag Singh");

            var result = await _container.CreateItemAsync(student);

            studentModel.UId = result.Resource.UId;

            return Ok(studentModel);
        }

        [HttpPost]
        public async Task<Student> GetStudentByUId(string studentUId)
        {
            var response = _container.GetItemLinqQueryable<Student>(true).Where(b => b.UId == studentUId && b.DocumentType == "student" && b.Active && !b.Archieved).FirstOrDefault();

           
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllStudent()
        {
            var response = _container.GetItemLinqQueryable<Student>(true).Where(b => b.DocumentType == "student" && b.Active && !b.Archieved).ToList().AsEnumerable();
            return Ok(response);
        }

        [HttpPost]

        public async Task<Student> UpdateStudent(StudentModel student)
        {
            //getting the existing record by uid
            var existingStudent = await GetStudentByUId(student.UId);

            //active=false & archieved=true
            existingStudent.Active = false;
            existingStudent.Archieved = true;

            //replace in db
            await _container.ReplaceItemAsync(existingStudent, existingStudent.Id);

            //re initializse
            existingStudent.Initialize(false, "student", "Anurag", "Anurag Singh");

            //map with model coming in payload
            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            existingStudent.Course = student.Course;
            existingStudent.RollNo = student.RollNo;
            existingStudent.Version = existingStudent.Version;

            //add updated record in db
            await _container.CreateItemAsync(existingStudent);

            return existingStudent;


        }
        [HttpPost]

        public async Task<string> DeleteStudent(string uid)
        {
            //getting the existing record by uid
            var existingStudent = await GetStudentByUId(uid);

            //active=false & archieved=true
            existingStudent.Active = false;
            existingStudent.Archieved = true;

            //replace in db
            await _container.ReplaceItemAsync(existingStudent, existingStudent.Id);

            //re initializse
            existingStudent.Initialize(false, "student", "Anurag", "Anurag Singh");

            existingStudent.Active = false;

            //add updated record in db
            //var response = await _container.CreateItemAsync(existingStudent);

            return "Record Deleted";


        }
    }
}

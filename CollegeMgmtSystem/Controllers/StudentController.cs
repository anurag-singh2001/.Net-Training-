using CollegeMgmtSystem.Interface;
using CollegeMgmtSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeMgmtSystem.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;

        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentModel studentModel)
        {
            var student = await _studentService.AddStudent(studentModel);
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllStudents()
        {
            var student = await _studentService.GetAllStudents();
            return Ok(student);
        }

        [HttpPost]

        public async Task<IActionResult> GetAllStudentByDeptUid(string Uid)
        {
            var student = await _studentService.GetAllStudentByDeptUid(Uid);
            return Ok(student);
        }

        [HttpPost]

        public async Task<IActionResult> GetStudentIDAsPdf(string Uid)
        {
            var path = await _studentService.GeneratePDF(Uid);
            return Ok(path);
        }

        [HttpPost]

        public async Task<IActionResult> GetStudentByUId(string UId)
        {
            var student = await _studentService.GetStudentByUid(UId);
            return Ok(student);
        }

        
        
    }
}

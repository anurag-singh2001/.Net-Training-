using CollegeMgmtSystem.common;
using CollegeMgmtSystem.Entity;
using CollegeMgmtSystem.Interface;
using CollegeMgmtSystem.Models;
using CollegeMgmtSystem.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace CollegeMgmtSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
       
        private IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
           
            _departmentService = departmentService;

        }

       

        [HttpPost]

        public async Task<IActionResult> AddDepartment(DepartmentModel departmentModel)
        {
            var department = await _departmentService.AddDepartment(departmentModel);
            return Ok(department);
        }

        [HttpPost]

        public async Task<IActionResult> GetAllDepartment()
        {
            var response = await _departmentService.GetAllDepartment();
            //var departmentModelList = new List<DepartmentModel>();

            //foreach(var department in response)
            //{
            //    departmentModelList.Add(department);
            //}
            //return departmentModelList;

            return Ok(response);
        }

        [HttpPost]

        public async Task<IActionResult> GetAllDepartmentByClgUid(string collegeUid)
        {
            var response = await _departmentService.GetAllDepartmentByClgUid(collegeUid);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetDepartmentByUid(string Uid)
        {
            var response = await _departmentService.GetDepartmentByUid(Uid);
            return Ok(response);

        }

    }
}

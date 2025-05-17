using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using OfficeOpenXml;
using StudentMgmtSys.Common;
using StudentMgmtSys.Entity;
using StudentMgmtSys.Interface;
using StudentMgmtSys.Models;
using StudentMgmtSys.Services;

namespace StudentMgmtSys.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private Container _container;
        private CosmosClient _cosmosClient;
        private IDepartmentService _departmentService;
        private IExcelService _excelService;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(CosmosClient client,IDepartmentService departmentService, IExcelService excelService, IWebHostEnvironment env)
        {
            _cosmosClient = new CosmosClient(CommonCredentials.CosmosURL, Environment.GetEnvironmentVariable("auth-token"));
            _container = _cosmosClient.GetContainer(Environment.GetEnvironmentVariable("database-name"), Environment.GetEnvironmentVariable("container-name"));

            _departmentService = departmentService;

            _excelService = excelService;
            _env = env;
        }

        public string DocumentType { get; private set; }

        [HttpPost]

        public async Task<IActionResult> AddDepartment(DepartmentModel departmentModel)
        {
            var department=await _departmentService.AddDepartment(departmentModel);
            return Ok(department);

        }

        [HttpPost]

        public async Task<IActionResult> AddMultipleDepartmenet(List<DepartmentModel> departmentModels)
        {
            var departments = await _departmentService.AddMultipleDepartmenet(departmentModels);
            return Ok(departments);
        }
 

        [HttpPost]
        public async Task<List<DepartmentModel>> GetAllDepartment()
        {
            var response = await _departmentService.GetAllDepartment();

            var departmentModelList=new List<DepartmentModel>();

            foreach (var department in response) 
            { 
                departmentModelList.Add(department);
            }

            return departmentModelList; 
        }

        [HttpPost]

        public async Task<DepartmentModel> GetDepartmentByUid(string departmentUid)
        {
            //var response = _container.GetItemLinqQueryable<Department>(true).Where(b => b.UId == departmentUid && DocumentType == "department" && b.Active && !b.Archieved).FirstOrDefault();
            var response = await _departmentService.GetDepartmentByUid(departmentUid);

            return response;
        }

        [HttpPost]

        public async Task<DepartmentModel> UpdateDepartment(DepartmentModel department)
        {
            
            var response = await _departmentService.UpdateDepartment(department);

            return response;

        }

        [HttpPost]
        public async Task<string> DeleteDepartment(string departmentUid)
        {
           
            var response = await _departmentService.DeleteDepartment(departmentUid);

            return response;

        }


        [HttpGet("save-excel")]

        public async Task<IActionResult> SaveExcelFile()
        {
            var departments = await _departmentService.GetAllDepartment();
            string folderPath = Path.Combine(_env.WebRootPath, "exports");
            var savedFilePath = await _excelService.SaveDepartmentExcelAsync(departments, folderPath);


            return Ok(new { message = "Excel file saved successfully.", filePath = savedFilePath });
        }



        
        


    }
}

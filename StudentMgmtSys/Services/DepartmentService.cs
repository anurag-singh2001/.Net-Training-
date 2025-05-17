using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentMgmtSys.CosmosDB;
using StudentMgmtSys.Entity;
using StudentMgmtSys.Interface;
using StudentMgmtSys.Models;

namespace StudentMgmtSys.Services
{
    public class DepartmentService : IDepartmentService
    {
        private ICosmosDBService _cosmosDbService;
        private IMapper _mapper;
        public DepartmentService(ICosmosDBService cosmosDBService,IMapper mapper) 
        { 
            _cosmosDbService = cosmosDBService;
            _mapper = mapper;
        
        }
        public async Task<DepartmentModel> AddDepartment(DepartmentModel departmentModel)
        {
            var department = _mapper.Map<Department>(departmentModel);
            department.Initialize(true, "department", "Anurag", "Anurag Singh");
            var result = await _cosmosDbService.AddDepartment(department);
            
            var responseModel=_mapper.Map<DepartmentModel>(result);
            return responseModel;   

        }


        public async Task<List<DepartmentModel>> AddMultipleDepartmenet(List<DepartmentModel> departmentModels)
        {
            
            var response = new List<DepartmentModel>();

            foreach(var department in departmentModels)
            {
                var addedDepartment = await AddDepartment(department);
                response.Add(addedDepartment);

            }

            return response;

        }


        public async Task<List<DepartmentModel>> GetAllDepartment()
        {
            var response=await _cosmosDbService.GetAllDepartment();

            var responseModel=_mapper.Map<List<DepartmentModel>>(response);

            return responseModel;

        }

        public async Task<DepartmentModel> GetDepartmentByUid(string departmentUid)
        {
            var response = await _cosmosDbService.GetDepartmentByUid(departmentUid);

            var responseModel = _mapper.Map<DepartmentModel>(response);

            return responseModel;

        }

        public async Task<DepartmentModel> UpdateDepartment(DepartmentModel department)
        {


            var existingDepartment = await _cosmosDbService.GetDepartmentByUid(department.UId);

            existingDepartment.Active = false;
            existingDepartment.Archieved = true;

            await _cosmosDbService.ReplaceAsync(existingDepartment);

            existingDepartment.Initialize(false, "department", "Anurag", "Anurag Singh");

            _mapper.Map(department, existingDepartment);

            var response = await _cosmosDbService.AddDepartment(existingDepartment);

            var responseModel = _mapper.Map<DepartmentModel>(response);

            return responseModel;


        }

        public async Task<string> DeleteDepartment(string departmentUid)
        {

            var department = await _cosmosDbService.GetDepartmentByUid(departmentUid);
            department.Active = false;
            department.Archieved = true;

            await _cosmosDbService.ReplaceAsync(department);

            department.Initialize(false, "department", "Anurag", "Anurag Singh");
            department.Active = false;

            await _cosmosDbService.AddDepartment(department);

            return " record deleted";
 

        }



    }
}

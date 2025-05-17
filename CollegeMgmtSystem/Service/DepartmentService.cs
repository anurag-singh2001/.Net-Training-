using AutoMapper;
using CollegeMgmtSystem.common;
using CollegeMgmtSystem.CosmoDB;
using CollegeMgmtSystem.Entity;
using CollegeMgmtSystem.Interface;
using CollegeMgmtSystem.Models;

namespace CollegeMgmtSystem.Service
{
    public class DepartmentService : IDepartmentService
    {

        private ICosmoDBService _cosmosDbService;
        private IMapper _mapper;

        public DepartmentService(ICosmoDBService cosmosDBService, IMapper mapper)
        {
            _cosmosDbService = cosmosDBService;
            _mapper = mapper;
        }

        public async Task<DepartmentModel> AddDepartment(DepartmentModel departmentModel)
        {
            var department = _mapper.Map<Department>(departmentModel);
            department.Initialize(true, CommonCredentials.DepartmentDocumentType, "Anurag", "Anurag Singh");
            var result = await _cosmosDbService.AddDepartment(department);

            var responseModel = _mapper.Map<DepartmentModel>(result);
            return responseModel;
        }

        public async Task<List<DepartmentModel>> GetAllDepartment()
        {

            var departments = await _cosmosDbService.GetAllDepartment();
            var departmentModels = new List<DepartmentModel>();

            foreach(var department in departments )
            {
                var students = await _cosmosDbService.GetAllStudentByDeptUid(department.UId);
                var departmentModel = _mapper.Map<DepartmentModel>(department);

                departmentModel.Count = students.Count;
                departmentModels.Add(departmentModel);
            }

            return departmentModels;

        }

        public async Task<List<DepartmentModel>> GetAllDepartmentByClgUid(string collegeUid)
        {
            //var response = await _cosmosDbService.GetAllDepartmentByClgUid(collegeUid);
            //var responseModel = _mapper.Map<List<DepartmentModel>>(response);
            //return responseModel;

            var departments = await _cosmosDbService.GetAllDepartmentByClgUid(collegeUid);
            var departmentModels = new List<DepartmentModel>();

            foreach (var department in departments)
            {
                var students = await _cosmosDbService.GetAllStudentByDeptUid(department.UId);
                var departmentModel = _mapper.Map<DepartmentModel>(department);

                departmentModel.Count = students.Count;
                departmentModels.Add(departmentModel);
            }

            return departmentModels;
        }

        public async Task<DepartmentModel> GetDepartmentByUid(string Uid)
        {
            var response = await _cosmosDbService.GetDepartmentByUid(Uid);
            var responseModel = _mapper.Map<DepartmentModel>(response);
            return responseModel;
        }

    }
}

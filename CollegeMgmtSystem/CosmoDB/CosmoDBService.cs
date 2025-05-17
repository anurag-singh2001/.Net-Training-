using CollegeMgmtSystem.common;
using CollegeMgmtSystem.Entity;
using CollegeMgmtSystem.Models;
using Microsoft.Azure.Cosmos;

namespace CollegeMgmtSystem.CosmoDB
{
    public class CosmoDBService : ICosmoDBService
    {

        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        
        public CosmoDBService()
        {
            _cosmosClient = new CosmosClient(CommonCredentials.CosmosURL, CommonCredentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(CommonCredentials.DataBaseName, CommonCredentials.ContainerName);
        }

        public async Task<College> AddCollege(College college)
        {
            var response =  await _container.CreateItemAsync(college);
            return response;
            
        }

        public async Task<Department> AddDepartment(Department department)
        {
            var response = await _container.CreateItemAsync(department);
            return response;
        }

        public async Task<Students> AddStudent(Students students)
        {
            var response = await _container.CreateItemAsync(students);
            return response;
        }

        public async Task<List<College>> GetAllCollege()
        {
            var response = _container.GetItemLinqQueryable<College>(true).Where(b => b.DocumentType == "college" && b.Active && !b.Archieved).ToList();
            return response;
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            var response = _container.GetItemLinqQueryable<Department>(true).Where(b => b.DocumentType == "department" && b.Active && !b.Archieved).ToList();
            return response;
        }

        public async Task<List<Department>> GetAllDepartmentByClgUid(string collegeUid)
        {
            var response = _container.GetItemLinqQueryable<Department>(true).Where(b => b.ClgUid == collegeUid && b.DocumentType == "department" && b.Active && !b.Archieved).AsEnumerable().ToList();

            return response;
        }

        public async Task<List<Students>> GetAllStudents()
        {
            var response = _container.GetItemLinqQueryable<Students>(true).Where(b => b.DocumentType == "student" && b.Active && !b.Archieved).ToList();
            return response;
        }

        public async Task<College> GetCollegeByUid(string clgUid)
        {
            var response = _container.GetItemLinqQueryable<College>(true).Where(b => b.UId == clgUid && b.DocumentType == "college" && b.Active && !b.Archieved).FirstOrDefault();

            return response;
        }

        public async Task<Department> GetDepartmentByUid(string Uid)
        {
            var response = _container.GetItemLinqQueryable<Department>(true).Where(b => b.UId == Uid && b.DocumentType == "department" && b.Active && !b.Archieved).FirstOrDefault();
            return response;

        }

        public async Task<List<Students>> GetAllStudentByDeptUid(string Uid)
        {
            var response = _container.GetItemLinqQueryable<Students>(true).Where(b => b.DeptUid == Uid && b.DocumentType == "student" && b.Active && !b.Archieved).AsEnumerable().ToList();

            return response;
        }

        public async Task<Students> GetStudentByUid(string Uid)
        {
            var response = _container.GetItemLinqQueryable<Students>(true).Where(b => b.UId == Uid && b.DocumentType == "student" && b.Active && !b.Archieved).FirstOrDefault();

            return response;
        }
    }
}

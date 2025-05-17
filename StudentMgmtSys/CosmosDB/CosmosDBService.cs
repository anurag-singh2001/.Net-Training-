using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using StudentMgmtSys.Common;
using StudentMgmtSys.Entity;
using StudentMgmtSys.Models;

namespace StudentMgmtSys.CosmosDB
{
    public class CosmosDBService : ICosmosDBService
    {
        public CosmosClient _cosmosClient;
        private readonly Container _container;
        public CosmosDBService()
        {
            _cosmosClient = new CosmosClient(CommonCredentials.CosmosURL, CommonCredentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(CommonCredentials.DataBaseName, CommonCredentials.ContainerName);
        }

        public async Task<Department> AddDepartment(Department department)
        {
            var response=await _container.CreateItemAsync(department);  
            return response;
        }

        

        public async Task<List<Department>> GetAllDepartment()
        {
            var response = _container.GetItemLinqQueryable<Department>(true).Where(b => b.DocumentType == "department" && b.Active && !b.Archieved).ToList();
            return response;
        }


        public async Task<Department> GetDepartmentByUid(string departmentUid)
        {
            var response = _container.GetItemLinqQueryable<Department>(true).Where(b => b.UId == departmentUid && b.DocumentType == "department" && b.Active && !b.Archieved).FirstOrDefault();

            return response;
        }


        public async Task ReplaceAsync(dynamic entity)
        {
            var response = await _container.ReplaceItemAsync(entity, entity.Id);

        }


    }
}

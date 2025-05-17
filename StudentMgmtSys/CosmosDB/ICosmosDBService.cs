using Microsoft.AspNetCore.Mvc;
using StudentMgmtSys.Entity;
using StudentMgmtSys.Models;

namespace StudentMgmtSys.CosmosDB
{
    public interface ICosmosDBService
    {
        Task<Department> AddDepartment(Department department);


        Task<List<Department>> GetAllDepartment();
        Task<Department> GetDepartmentByUid(string departmentUid);
        //Task<Department> UpdateDepartment(Department department);

        //Task<Department> DeleteDepartment(string departmentUid);

        Task ReplaceAsync(dynamic entity);

    }
}
    
using Microsoft.AspNetCore.Mvc;
using StudentMgmtSys.Entity;
using StudentMgmtSys.Models;

namespace StudentMgmtSys.Interface
{
    public interface IDepartmentService
    {
        Task<DepartmentModel> AddDepartment(DepartmentModel departmentModel);

        Task<List<DepartmentModel>> AddMultipleDepartmenet(List<DepartmentModel> departmentModels);

        Task<List<DepartmentModel>> GetAllDepartment();
        Task<DepartmentModel> GetDepartmentByUid(string departmentUid);
        Task<DepartmentModel> UpdateDepartment(DepartmentModel department);

        Task<string> DeleteDepartment(string departmentUid);

        
    }
}

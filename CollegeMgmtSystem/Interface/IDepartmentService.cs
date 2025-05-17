using CollegeMgmtSystem.Models;

namespace CollegeMgmtSystem.Interface
{
    public interface IDepartmentService
    {
        Task<DepartmentModel> AddDepartment(DepartmentModel departmentModel);
        Task<List<DepartmentModel>> GetAllDepartment();

        Task<List<DepartmentModel>> GetAllDepartmentByClgUid(string collegeUid);

        Task<DepartmentModel> GetDepartmentByUid(string Uid);
    }
}

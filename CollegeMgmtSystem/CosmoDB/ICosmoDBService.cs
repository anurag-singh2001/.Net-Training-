using CollegeMgmtSystem.Entity;
using CollegeMgmtSystem.Models;

namespace CollegeMgmtSystem.CosmoDB
{
    public interface ICosmoDBService
    {
        Task<College> AddCollege(College college);
        Task<Department> AddDepartment(Department department);
        Task<Students> AddStudent(Students students);

        Task<List<College>> GetAllCollege();

        Task<List<Department>> GetAllDepartment();

        Task<List<Department>> GetAllDepartmentByClgUid(string collegeUid);

        Task<List<Students>> GetAllStudents();

        Task<College> GetCollegeByUid(string clgUid);

        Task<Department> GetDepartmentByUid(string Uid);

        Task<List<Students>> GetAllStudentByDeptUid(string Uid);

        Task<Students> GetStudentByUid(string Uid);

    }
}

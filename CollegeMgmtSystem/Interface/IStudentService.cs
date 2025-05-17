using CollegeMgmtSystem.Entity;
using CollegeMgmtSystem.Models;

namespace CollegeMgmtSystem.Interface
{
    public interface IStudentService
    {
        Task<StudentModel> AddStudent(StudentModel studentModel);

        Task<List<StudentModel>> GetAllStudents();

        Task<List<StudentModel>> GetAllStudentByDeptUid(string Uid);

        Task<string> GeneratePDF(string UId);

        Task<StudentModel> GetStudentByUid(string UId);
    }
}

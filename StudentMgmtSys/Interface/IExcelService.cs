using StudentMgmtSys.Models;

namespace StudentMgmtSys.Interface
{
    public interface IExcelService
    {
        Task<string> SaveDepartmentExcelAsync(List<DepartmentModel> departments, string folderPath);
    }
}

using OfficeOpenXml;
using StudentMgmtSys.Entity;
using StudentMgmtSys.Interface;
using StudentMgmtSys.Models;

namespace StudentMgmtSys.Services
{
    public class ExcelService : IExcelService
    {
        public async Task<string> SaveDepartmentExcelAsync(List<DepartmentModel> departments, string folderPath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filename = "Department.xlsx";
            string fullpath = Path.Combine(folderPath, filename);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Departments");

                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1,2].Value= "Name";
                worksheet.Cells[1, 3].Value = "Count";

                int row = 2;

                foreach(var department in departments)
                {
                    worksheet.Cells[row, 1].Value = department.UId;
                    worksheet.Cells[row, 2].Value = department.Dept;
                    worksheet.Cells[row, 3].Value = department.Count;

                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var fileInfo = new FileInfo(fullpath);

                await package.SaveAsAsync(fileInfo);
            }

            return fullpath;
                
        }

    }


    
}

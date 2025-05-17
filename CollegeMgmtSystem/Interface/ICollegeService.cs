using CollegeMgmtSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeMgmtSystem.Interface
{
    public interface ICollegeService
    {
        Task<CollegeModel> AddCollege(CollegeModel collegeModel);
        Task<List<CollegeModel>> GetAllCollege();

        Task<CollegeModel> GetCollegeByUid(string clgUid);

        

    }
}

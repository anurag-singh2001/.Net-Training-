using CollegeMgmtSystem.common;
using CollegeMgmtSystem.Interface;
using CollegeMgmtSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace CollegeMgmtSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        
        private ICollegeService _collegeService;
       
        public CollegeController(ICollegeService collegeService)
        {
            _collegeService = collegeService;

        }

       

        [HttpPost]

        public async Task<IActionResult> AddCollege(CollegeModel collegeModel)
        {
            var college = await _collegeService.AddCollege(collegeModel);
            return Ok(college);
        }

        [HttpPost]

        public async Task<List<CollegeModel>> GetAllCollege()
        {
            var response = await _collegeService.GetAllCollege();
            var collegeModelList = new List<CollegeModel>();

            foreach(var college in response)
            {
                collegeModelList.Add(college);
            }

            return collegeModelList;

        }

        [HttpPost]

        public async Task<IActionResult> GetCollegeByUid(string clgUid)
        {
            var college = await _collegeService.GetCollegeByUid(clgUid);
            return Ok(college);
        }






    }
}

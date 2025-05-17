using System.Net;
using AutoMapper;
using CollegeMgmtSystem.common;
using CollegeMgmtSystem.CosmoDB;
using CollegeMgmtSystem.Entity;
using CollegeMgmtSystem.Interface;
using CollegeMgmtSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeMgmtSystem.Service
{
    public class CollegeService : ICollegeService
    {
        private ICosmoDBService _cosmosDbService;
        private IMapper _mapper;

        public CollegeService(ICosmoDBService cosmosDBService, IMapper mapper)
        {
            _cosmosDbService = cosmosDBService;
            _mapper = mapper;
        }

        public async Task<CollegeModel> AddCollege(CollegeModel collegeModel)
        {
            var college = _mapper.Map<College>(collegeModel);
            college.Initialize(true, CommonCredentials.CollegeDocumentType, "Anurag", "Anurag Singh");
            var result = await _cosmosDbService.AddCollege(college);

            var responseModel = _mapper.Map<CollegeModel>(result);
            return responseModel;
        }

        public async Task<List<CollegeModel>> GetAllCollege()
        {
            var collegeEntities = await _cosmosDbService.GetAllCollege();
            var collegeModels = new List<CollegeModel>();

            foreach (var college in collegeEntities)
            {
               
                var departmentEntities = await _cosmosDbService.GetAllDepartmentByClgUid(college.UId);

                var departmentNames = departmentEntities.Select(d => d.Name).ToList();

                var collegeModel = _mapper.Map<CollegeModel>(college);

                collegeModel.Dept = departmentNames;

                collegeModels.Add(collegeModel);
            }

            return collegeModels;
        }

        public async Task<CollegeModel> GetCollegeByUid(string clgUid)
        {
            var collegeEntity = await _cosmosDbService.GetCollegeByUid(clgUid);

            var collegeModel = _mapper.Map<CollegeModel>(collegeEntity);

            var departments = await _cosmosDbService.GetAllDepartmentByClgUid(clgUid);

            collegeModel.Dept = departments.Select(d => d.Name).ToList();

            return collegeModel;
        }
    }
}

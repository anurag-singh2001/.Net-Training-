using AutoMapper;
using CollegeMgmtSystem.Entity;
using CollegeMgmtSystem.Models;

namespace StudentMgmtSys.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<College, CollegeModel>().ReverseMap();
            CreateMap<Department, DepartmentModel>().ReverseMap();
            CreateMap<Students, StudentModel>().ReverseMap();
        }

    }
}

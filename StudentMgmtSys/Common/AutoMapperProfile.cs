using StudentMgmtSys.Entity;
using StudentMgmtSys.Models;
using AutoMapper;
using System.Runtime.CompilerServices;

namespace StudentMgmtSys.Common
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Department, DepartmentModel>().ReverseMap();
        }

    }
}

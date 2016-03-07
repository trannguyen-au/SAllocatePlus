using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SwinSchool.DAL.Entities;
using SwinSchool.CommonShared.Dto;

namespace SwinSchool.Services.Mappers
{
    public class MapperConfig
    {
        internal static void RegisterConfiguration()
        {
            AutoMapper.Mapper.CreateMap<MyUser, MyUserDto>();
            AutoMapper.Mapper.CreateMap<MyUserDto, MyUser>();
            AutoMapper.Mapper.CreateMap<IEnumerable<MyUser>, IEnumerable<MyUserDto>>();
            AutoMapper.Mapper.CreateMap<IEnumerable<MyUserDto>, IEnumerable<MyUser>>();
        }
    }
}

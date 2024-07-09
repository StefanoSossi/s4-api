using System;
using AutoMapper;
using s4.Data.Models;


namespace s4.Logic.Models.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            this.CreateMap<Class, ClassDto>().ReverseMap();

            this.CreateMap<Student, StudentDto>().ReverseMap();

            this.CreateMap<StudentClass, StudentClassDto>().ReverseMap();
        }
    }
}

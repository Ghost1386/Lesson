using AutoMapper;
using Lesson.Common.Dto;
using Lesson.Models;

namespace Lesson.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserCreateDto, User>();
    }
}
﻿using AutoMapper;
using Lesson.BusinessLogic.Interfaces;
using Lesson.Common.Dto;
using Lesson.Models;

namespace Lesson.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly ApplicationContext _applicationContext;
    private readonly IMapper _mapper;

    public UserService(ApplicationContext applicationContext, IMapper mapper)
    {
        _applicationContext = applicationContext;
        _mapper = mapper;
    }

    public void Create(UserCreateDto userCreateDto)
    {
        var user = _mapper.Map<UserCreateDto, User>(userCreateDto);

        _applicationContext.Users.Add(user);
        _applicationContext.SaveChanges();
    }

    public List<User> GetAll()
    {
        var users = _applicationContext.Users.ToList();
        
        return users;
    }
    
    public User Get(UserAuthDto userAuthDto)
    {
        var user = _applicationContext.Users.FirstOrDefault(u =>
            u.Email == userAuthDto.Email && u.Password == userAuthDto.Password);

        return user;
    }
}
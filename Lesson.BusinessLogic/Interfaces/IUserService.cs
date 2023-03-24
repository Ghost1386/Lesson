using Lesson.Common.Dto;
using Lesson.Models;

namespace Lesson.BusinessLogic.Interfaces;

public interface IUserService
{
    void Create(UserCreateDto userCreateDto);

    List<User> GetAll();

    User Get(UserAuthDto userAuthDto);
}
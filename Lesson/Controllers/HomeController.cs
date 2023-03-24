using System.Security.Claims;
using Lesson.BusinessLogic.Interfaces;
using Lesson.Common.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Lesson.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService _userService;

    public HomeController(ILogger<HomeController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Auth(UserAuthDto userAuthDto)
    {
        var user = _userService.Get(userAuthDto);

        if (user != null)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity));
            
            _logger.LogInformation($"{DateTime.Now}: user with {userAuthDto.Email} is log in");

            return RedirectToAction("GetUsers");
        }

        return RedirectToAction("Auth");
    }

    public IActionResult CreateUser()
    {
        return View();
    }
    
    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }
    
    [HttpPost]
    public IActionResult CreateUser(UserCreateDto userCreateDto)
    {
        _userService.Create(userCreateDto);

        return RedirectToAction("Index", "Home");
    }
    
    [Authorize]
    public IActionResult GetUsers()
    {
        return View(_userService.GetAll());
    }
}
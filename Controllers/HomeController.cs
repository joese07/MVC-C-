using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Context;
using MVC.Models;
using MVC.ViewModels;

namespace MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    MyContext myContext;

    public HomeController(ILogger<HomeController> logger, MyContext myContext)
    {
        _logger = logger;
        this.myContext = myContext;
    }

    public IActionResult Index()
    {
       var fullname =  HttpContext.Session.GetString("Fullname");
        var email = HttpContext.Session.GetString("Email");
        var role = HttpContext.Session.GetString("Role");
        ResponseLogin responseLogin = new ResponseLogin()
        {
            FullName = fullname,
            Email = email,
            Role = role,

        };
      
        return View(responseLogin);
    }

    [HttpPost]
    public IActionResult SignOut()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("Login", "Auth");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


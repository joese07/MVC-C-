using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Context;
using MVC.Models;

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

    public IActionResult Index(int id)
    {
        var data = myContext.Employees.Find(1);
        return View(data);
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


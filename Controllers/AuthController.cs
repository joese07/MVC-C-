using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.Context;
using MVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC.Controllers
{


    public class AuthController : Controller
    {

        MyContext myContext;

        public AuthController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        // GET: /<controller>/
        public IActionResult Index(int id )
        {
            var data = myContext.Employees.Find(1);
            return View(data);
        }


        public IActionResult Login()
        {
     
            return View();
        }


       //https://stackoverflow.com/questions/72848070/login-page-with-asp-net-core-mvc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM loginVM)
        {
       
            var Data = from m in myContext.Employees select m;
            var DataP = from p in myContext.Users select p;
            Data = Data.Where(s => s.Email.Contains(loginVM.employee.Email));
            if(Data.Count() != 0)
            {
                if(DataP.First().Password == loginVM.user.Password)
                {
                    return RedirectToAction("Index", "Auth");
                }
            }
               
           
            return RedirectToAction("Fail");
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Fail()
        {
            return View();
        }
    }
}


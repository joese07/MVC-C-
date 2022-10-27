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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Employee employee)
        {
         if (ModelState.IsValid)
            {
                var Data = from m in myContext.Employees select m;
                Data = Data.Where(s => s.Email.Contains(employee.Email));
                if (Data.Count() != 0)
                {
                    if (Data.First().Email == employee.Email)
                    {
                        return RedirectToAction("Index");
                    }
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


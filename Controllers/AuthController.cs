using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Context;
using MVC.Models;
using MVC.ViewModels;

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
        public IActionResult Index()
        {
            //var idLogin = 
       
            return View();
        }


        public IActionResult Login()
        {
     
            return View();
        }


        //https://stackoverflow.com/questions/72848070/login-page-with-asp-net-core-mvc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        { 
            var data = myContext.Users
                .Include(x => x.Employee)
                .Include(x => x.Roles)
                .SingleOrDefault(x => x.Employee.Email.Equals(email) && x.Password.Equals(password));

            if(data != null)
            {
                User user = new User()
                {
                    Password = data.Password
                };

                ResponseLogin responseLogin = new ResponseLogin()
                {
                    FullName = data.Employee.FullName,
                    Email = data.Employee.Email,
                    Role = data.Roles.Name
                };

                return RedirectToAction("Index", "Home", responseLogin);


            }


            return View();

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullName, string email, string birthDate, string password)
        {
            Employee employee = new Employee()
            {
                FullName = fullName,
                Email = email,
                BirthDate = birthDate
            };


            myContext.Employees.Add(employee);
            var result = myContext.SaveChanges();
            if(result > 0)
            {
                var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
                User user = new User()
                {
                    Id = id,
                    Password = password,
                    RoleId = 1,
                };

                myContext.Users.Add(user);
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                    return RedirectToAction("Login", "Auth");
                
            }
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string fullName, string email, string birthDate)
        {
            var data = myContext.Users
                          .Include(x => x.Employee)
                          .SingleOrDefault(x => x.Employee.Email
                          .Equals(email) && x.Employee.FullName.Equals(fullName) && x.Employee.BirthDate.Equals(birthDate));

            if (data != null)
            {
                Employee employee = new Employee()
                {
                    FullName = fullName,
                    Email = email,
                    BirthDate = birthDate
                };


                return RedirectToAction("NewPassword", "Auth");
            }


            return View();
        }
        public IActionResult NewPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewPassword(string password, Employee employee, User user)
        {
            var data = myContext.Users.Find(employee.Email);
            if(data != null)
            {
                data.Password = user.Password;
                myContext.Entry(data).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ChangePassword( string OldPassword, User user)
        {
            var data = myContext.Users.SingleOrDefault(x => x.Password.Equals(OldPassword));

            if (data != null)
            {
                if( data.Password == OldPassword)
                {
                    data.Password = user.Password;
                    myContext.Entry(data).State = EntityState.Modified;
                    var result = myContext.SaveChanges();
                    if(result > 0)
                        return RedirectToAction("Login");
                }
            }

            return View();
        }
    }
}


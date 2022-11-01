using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Context;
using MVC.Handlers;
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
                .SingleOrDefault(x => x.Employee.Email.Equals(email));

            if(data != null)
            {
                if(Hashing.ValidatePassword(password, data.Password))
                {

                    HttpContext.Session.SetInt32("Id", data.Id);
                    HttpContext.Session.SetString("Fullname", data.Employee.FullName);
                    HttpContext.Session.SetString("Email", data.Employee.Email);
                    HttpContext.Session.SetString("Role", data.Roles.Name);


                    return RedirectToAction("Index", "Home");


                }

                ViewBag.Message = string.Format("Email atau Password salah");
                return View();

            }

            ViewBag.Message = string.Format("Email atau Password salah");
            return View();

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullName, string email, string birthDate, string password, string retypePassword)
        {
            if(retypePassword == password)
            {
                Employee employee = new Employee()
                {
                    FullName = fullName,
                    Email = email,
                    BirthDate = birthDate
                };


                myContext.Employees.Add(employee);
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
                    User user = new User()
                    {
                        Id = id,
                        Password = Hashing.HashPassword(password),
                        RoleId = 2,
                    };

                    myContext.Users.Add(user);
                    var resultUser = myContext.SaveChanges();
                    if (resultUser > 0)
                        return RedirectToAction("Login", "Auth");

                }
                ViewBag.Message = string.Format("Something wrong..");
                return View();

            }
            ViewBag.Message = string.Format("Retype Password tidak sama");
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

                User user = new User()
                {
                    Id = data.Id
                };

                return RedirectToAction("NewPassword", user);
            }


            return View();
        }
        public IActionResult NewPassword(int id)
        {
            var data = myContext.Users.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewPassword(int id, User user, string retypePassword)
        {
            try
            {
                var data = myContext.Users.Find(id);

              

                if (data != null)
                {
                    if(user.Password == retypePassword)
                    {
                        data.Password = Hashing.HashPassword(user.Password);
                        myContext.Entry(data).State = EntityState.Modified;
                        var result = myContext.SaveChanges();
                        if (result > 0)
                            return RedirectToAction("Login");
                    }
                    ViewBag.Message = string.Format("Retype Password tidak sama");
                    return View();

                }

                return View();

            } catch (Exception Ex)
            {
                return View(Ex);
            }
           
        }

        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ChangePassword( string OldPassword, string retypePassword, User user)
        {
            var email = HttpContext.Session.GetString("Email");
            var data = myContext.Users.Include(x => x.Employee).SingleOrDefault(x => x.Employee.Email.Equals(email));

            if (data != null)
            {
                
                    if (user.Password == retypePassword)
                    {
                        if (Hashing.ValidatePassword(OldPassword, data.Password))
                        {
                            data.Password = Hashing.HashPassword(user.Password);
                            myContext.Entry(data).State = EntityState.Modified;
                            var result = myContext.SaveChanges();
                            if (result > 0)
                                return RedirectToAction("Login");
                        }
                    ViewBag.Message = string.Format("Password lama tidak sesuai");
                    return View();

                }
                

                ViewBag.Message = string.Format("Retype Password tidak sama");
                return View();
            }

            ViewBag.Message = string.Format("Something Wrong..");
            return View();
        }
    }
}


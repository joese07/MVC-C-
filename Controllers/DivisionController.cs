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
    public class DivisionController : Controller
    {
        MyContext myContext;

        public DivisionController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        //GET ALL 
        // GET: /<controller>/
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if(role == "admin")
            {
                var data = myContext.Divisions.ToList();
                return View(data);
            }
            else if(role == null)
            {
                return RedirectToAction("UnAuthorized", "ErrorPage");
            }

            return RedirectToAction("Forbidden", "ErrorPage");
        }

        //GET BY ID
        public IActionResult Details(int id)
        {
            var data = myContext.Divisions.Find(id);
            return View(data); 
        }


        ////INSERT - GET POST

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Division division)
        {
            myContext.Divisions.Add(division);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Division");
            return View();
        }

        //UPDATE - GET POST
        public IActionResult Edit(int id)
        {
            var data = myContext.Divisions.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Division division)
        {
            var data = myContext.Divisions.Find(id);
            if(data != null)
            {
                data.Name = division.Name;
                myContext.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction("Index", "Division");
            }

            return View();
        }

        //DELETE
        public IActionResult Delete(int id)
        {
            var data = myContext.Divisions.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Division division)
        {
            myContext.Divisions.Remove(division);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Division");
            return View();
        }

    }
}


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
    public class DepartementController : Controller
    {
        MyContext myContext;

        public DepartementController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            var data = myContext.Departements.ToList();
            return View(data);
        }

        public IActionResult Details(int id)
        {
            var data = myContext.Departements.Find(id);
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Departement departement)
        {
            myContext.Departements.Add(departement);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Departement");
            return View();
        }

        public IActionResult Edit(int id)
        {
            var data = myContext.Departements.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Departement departement)
        {
            var data = myContext.Departements.Find(id);
            if(data != null)
            {
                data.Name = departement.Name;
                data.DivisionId = departement.DivisionId;
                myContext.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction("Index", "Departement");

            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            var data = myContext.Departements.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Departement departement)
        {
            myContext.Departements.Remove(departement);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Departement");
            return View();
        }

    }
}


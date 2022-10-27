using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Models
{
    public class MyViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DivisionId { get; set; }

        public List<SelectListItem> Divisions { get; set; }
    }
}


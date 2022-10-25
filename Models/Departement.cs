using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class Departement
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Division ID")]
        public int DivisionId { get; set; }

        [ForeignKey("DivisionId")]
        public virtual Division Divisions { get; set; }
    }
}


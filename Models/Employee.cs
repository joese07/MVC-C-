using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string BirthDate { get; set; }
    }
}


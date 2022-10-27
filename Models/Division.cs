using System;
namespace MVC.Models
{
    public class Division
    {
        public Division(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public Division()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }

     
    }
}


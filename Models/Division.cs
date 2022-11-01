using System;
using MVC.Base;

namespace MVC.Models
{
    public class Division : BaseModel
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


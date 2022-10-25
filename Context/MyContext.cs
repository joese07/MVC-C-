using System;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> dbContextOptions) : base(dbContextOptions)
        {
        }


        public DbSet<Division> Divisions { get; set; }

        public DbSet<Departement> Departements { get; set; }
       
        }
    }



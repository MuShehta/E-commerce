using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class AppContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer("Data Source=DESKTOP-ELJAV78\\SQLEXPRESS;Initial Catalog=E_commerce;Integrated Security=True");
        }

        public DbSet<User> users { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}

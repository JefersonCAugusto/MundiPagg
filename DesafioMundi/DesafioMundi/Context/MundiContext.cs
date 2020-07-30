using DesafioMundi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundi.Context
{
    public class MundiContext : DbContext
    {
        public MundiContext()
        {
        }

        public DbSet<Customer> Customers { get; set;}
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }

    }
}

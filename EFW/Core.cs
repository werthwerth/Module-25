﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Module_25.EFW.Program;

namespace Module_25.EFW
{
    public class ApplicationContext : DbContext
    {
        // Объекты таблицы Users
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Module-25.db");
        }
    }
    internal class DB
    {
        protected internal ApplicationContext context;
        protected internal DB()
        {
            context = new ApplicationContext();
            context.Database.EnsureCreated();
            context.Users.Load();
            context.Books.Load();
        }
    }
}

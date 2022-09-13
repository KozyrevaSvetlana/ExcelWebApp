﻿using PersonDb.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace PersonDb.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var newPersons = PersonGenerator.GeneradeRandomPersons(20);
            modelBuilder.Entity<Person>().HasData(newPersons);
        }
    }
}
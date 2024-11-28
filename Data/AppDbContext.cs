﻿using CarRental_System.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        //}
    }
}

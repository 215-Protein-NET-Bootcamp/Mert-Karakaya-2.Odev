using System;
using System.Collections.Generic;
using System.Text;
using CompanyAPI.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Data.Context
{
    public class AppDbContext : DbContext
    {
        static private string _connectionstring;

        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        static public void SetContextConnectionString(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionstring);
            }
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Folder> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(r => r.EmpId);
                entity.ToTable("employee");
                entity.HasOne(r => r.Department).WithMany(r => r.Employees).HasForeignKey(r => r.DeptId);
            });
            modelBuilder.Entity<Folder>(entity =>
            {
                entity.ToTable("folder");
                entity.HasOne(r => r.Employee).WithMany(r => r.Folders).HasForeignKey(r => r.EmpId);
            });
        }
    }
}

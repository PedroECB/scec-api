using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SCEC.API.Models;

namespace SCEC.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(@"Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase");
        }

        public DbSet<User> Users {get; set;}
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UsersRoles { get; set; }
        public DbSet<LogAcess> logAcesses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleRole> ModulesRoles { get; set; }
        public DbSet<EmailModel> EmailModels { get; set; }
    }
}

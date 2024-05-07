using EquipmentAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace EquipmentAPI.DataAccessLayer
{
    public class ApplicationDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public ApplicationDBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("cs"));
        }
        public virtual DbSet<Equipment> Equipment { get; set; }

    }
}

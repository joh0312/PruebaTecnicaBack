using Microsoft.EntityFrameworkCore;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> users { get; set; }
        //public DbSet<Articulos> Articuloss { get; set; }
        public DbSet<Drivers> drivers { get; set; }
        public DbSet<Schedules> schedules { get; set; }
        public DbSet<Routes> route { get; set; }
        public DbSet<Vehicles> vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Routes>()
                .HasOne(r => r.Driver)
                .WithMany()
                .HasForeignKey(r => r.driver_id);

            modelBuilder.Entity<Routes>()
                .HasOne(r => r.Vehicle)
                .WithMany()
                .HasForeignKey(r => r.vehicle_id);

            
        }
    }
}

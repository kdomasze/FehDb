using FehDb.API.Models.Entity;
using FehDb.API.Models.Entity.WeaponModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Contexts
{
    public class FehContext : DbContext
    {
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<WeaponStatChange> StatChanges { get; set; }
        public DbSet<WeaponCost> WeaponCosts { get; set; }
        public DbSet<WeaponEffectiveAgainst> WeaponEffectiveAgainsts { get; set; }

        public DbSet<WeaponEffectiveAgainstMovementType> WeaponEffectiveAgainstMovementTypes { get; set; }
        public DbSet<WeaponEffectiveAgainstWeaponType> WeaponEffectiveAgainstWeaponType { get; set; }
        
        public DbSet<WeaponType> WeaponTypes { get; set; }
        public DbSet<MovementType> MovementTypes { get; set; }

        public FehContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FehDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeaponEffectiveAgainstMovementType>()
                .HasOne(weawt => weawt.MovementType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WeaponEffectiveAgainstWeaponType>()
                .HasOne(weawt => weawt.WeaponType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Weapon>()
                .HasOne(w => w.WeaponType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

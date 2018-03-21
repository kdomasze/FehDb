using FehDb.API.Models.Entity;
using FehDb.API.Models.Entity.UserModel;
using FehDb.API.Models.Entity.WeaponModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FehDb.API.Contexts
{
    public class FehContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Weapon> Weapons { get; set; }
        public virtual DbSet<WeaponStatChange> StatChanges { get; set; }
        public virtual DbSet<WeaponCost> WeaponCosts { get; set; }
        public virtual DbSet<WeaponEffectiveAgainst> WeaponEffectiveAgainsts { get; set; }

        public virtual DbSet<WeaponEffectiveAgainstMovementType> WeaponEffectiveAgainstMovementTypes { get; set; }
        public virtual DbSet<WeaponEffectiveAgainstWeaponType> WeaponEffectiveAgainstWeaponType { get; set; }

        public virtual DbSet<WeaponType> WeaponTypes { get; set; }
        public virtual DbSet<MovementType> MovementTypes { get; set; }

        public FehContext(DbContextOptions options) : base(options) { }
        public FehContext() { }

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

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
        
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).DateAdded = DateTime.UtcNow;
                }

                ((BaseEntity)entity.Entity).DateModified = DateTime.UtcNow;
            }
        }
    }
}

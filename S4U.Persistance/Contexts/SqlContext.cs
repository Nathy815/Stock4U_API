using Microsoft.EntityFrameworkCore;
using S4U.Domain.Entities;
using S4U.Persistance.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Persistance.Contexts
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Signature> Signatures { get; set; }
        public DbSet<Equity> Equities { get; set; }
        public DbSet<UserEquity> UserEquities { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<CompareEquity> CompareEquities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new PlanMap());
            modelBuilder.ApplyConfiguration(new EquityMap());
            modelBuilder.ApplyConfiguration(new CompareEquityMap());
            modelBuilder.ApplyConfiguration(new UserEquityMap());
            modelBuilder.ApplyConfiguration(new NoteMap());
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = ChangeTracker.Entries().Where(e => e.Entity is Base && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Base)entity.Entity).CreatedDate = DateTime.Now;
                    ((Base)entity.Entity).Deleted = false;
                }
                else if (entity.State == EntityState.Deleted)
                    ((Base)entity.Entity).Deleted = true;

                ((Base)entity.Entity).ModifiedDate = DateTime.Now;
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
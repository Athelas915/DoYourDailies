using DoYourDailies.Data.Entities;
using DoYourDailies.Data.Entities.Identity;
using DoYourDailies.Data.Extensions;
using DoYourDailies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace DoYourDailies.Data.Implementations
{
    public class DoYourDailiesContext(DbContextOptions<DoYourDailiesContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<DailyTask> DailyTasks { get; set; }
        public DbSet<WeeklyTask> WeeklyTasks { get; set; }
        public DbSet<MonthlyTask> MonthlyTasks { get; set; }
        public DbSet<YearlyTask> YearlyTasks { get; set; }
        public DbSet<OneTimeTask> OneTimeTasks { get; set; }
        public DbSet<ASAPTask> ASAPTasks { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasOne(au => au.User)
                .WithOne(u => u.AppUser)
                .HasForeignKey<AppUser>(au => au.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasMany(au => au.DailyTasks)
                .WithOne(t => t.AppUser)
                .HasForeignKey(t => t.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasMany(au => au.WeeklyTask)
                .WithOne(t => t.AppUser)
                .HasForeignKey(t => t.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasMany(au => au.MonthlyTask)
                .WithOne(t => t.AppUser)
                .HasForeignKey(t => t.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasMany(au => au.YearlyTask)
                .WithOne(t => t.AppUser)
                .HasForeignKey(t => t.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasMany(au => au.OneTimeTask)
                .WithOne(t => t.AppUser)
                .HasForeignKey(t => t.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasMany(au => au.ASAPTask)
                .WithOne(t => t.AppUser)
                .HasForeignKey(t => t.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override int SaveChanges()
        {
            AddTimes();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddTimes();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await AddTimesAsync();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await AddTimesAsync();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AddTimes()
        {
            var date = this.GetCurrentTimeFromDb();
            AddCreatedAndUpdatedTimes(date);
        }

        private async Task AddTimesAsync()
        {
            var date = await this.GetCurrentTimeFromDbAsync();
            AddCreatedAndUpdatedTimes(date);
        }

        private void AddCreatedAndUpdatedTimes(DateTime currentTime)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IEntity e)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property(nameof(IEntity.CreatedOn)).CurrentValue = currentTime;
                        entry.Property(nameof(IEntity.LastUpdatedOn)).CurrentValue = currentTime;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entry.Property(nameof(IEntity.LastUpdatedOn)).CurrentValue = currentTime;
                    }
                }
            }
        }
    }
}

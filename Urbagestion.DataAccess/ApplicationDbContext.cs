using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Urbagestion.Model.Common;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;

namespace Urbagestion.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IUnitOfWork
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public EntityEntry<T> EntityEntry<T>(T entity) where T : Entity
        {
            return Entry(entity);
        }

        public int Complete()
        {
            return SaveChanges();
        }

        public DbSet<T> GetDbSet<T>() where T : Entity
        {
            return Set<T>();
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void SetDeleted(object entity)
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void SetAdded(object entity)
        {
            Entry(entity).State = EntityState.Added;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Many to many join table
            builder.Entity<UserGroup>().HasKey(k => new {k.UserId, k.GroupId});
            // Unique indexes
            builder.Entity<Facility>().HasIndex(f => f.Name).HasName($"UX_{nameof(Facility)}_{nameof(Facility.Name)}").IsUnique();
        }
    }
}
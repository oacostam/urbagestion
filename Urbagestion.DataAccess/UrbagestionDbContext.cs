using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;

namespace Urbagestion.DataAccess
{
    public class UrbagestionDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IUnitOfWork
    {

        public UrbagestionDbContext(DbContextOptions<UrbagestionDbContext> options) : base(options)
        {
        }
        
        public int Complete()
        {
            return SaveChanges();
        }

        public IQueryable<T> GetEntitySet<T>() where T : class, IHasIdentity
        {
            return Set<T>();
        }
        

        public new T Update<T>(T entity) where T : class, IHasIdentity
        {
            Set<T>().Update(entity);
            return entity;
        }

        public void Delete<T>(T entity) where T : class, IHasIdentity
        {
            Set<T>().Remove(entity);
        }

        public new T Add<T>(T entity) where T : class, IHasIdentity
        {
            Set<T>().Add(entity);
            return entity;
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
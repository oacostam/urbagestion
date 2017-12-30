using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Urbagestion.Model.Common;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;

namespace Urbagestion.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
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

        public IQueryable<T> GetDbSet<T>() where T : Entity
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
    }
}
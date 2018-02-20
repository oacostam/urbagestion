using System.Linq;
using System.Security.Principal;
using Urbagestion.Model.Bussines.Common;
using Urbagestion.Model.Bussines.Interfaces;
using Urbagestion.Model.Common;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;

namespace Urbagestion.Model.Bussines.Implementation
{
    public class FacilityManagement : BaseService<Facility>, IFacilityManagement
    {
        public FacilityManagement(IUnitOfWork unitOfWork, IPrincipal principal) : base(unitOfWork, principal)
        {
        }

        public new Facility Create(Facility entity)
        {
            CheckNotNullAndAdminRigths(entity, Principal);
            var otherWithSameName = UnitOfWork.GetEntitySet<Facility>().FirstOrDefault(f => f.Name == entity.Name);
            if (otherWithSameName != null)
                throw new BussinesException(
                    "Ya existe una instalación con ese nombre, por favor, elija un nombre único.");
            entity = base.Create(entity);
            Complete();
            return entity;
        }

        public void Delete(int id)
        {
            var facility = GetById(id);
            CheckNotNullAndAdminRigths(facility, Principal);
            if (UnitOfWork.GetEntitySet<Reservation>().Any(r => r.Facility.Id == facility.Id))
            {
                facility.IsActive = false;
                Update(facility);
            }
            else
            {
                base.Delete(facility);
            }
            Complete();
        }


        public new Facility Update(Facility facility)
        {
            CheckNotNullAndAdminRigths(facility, Principal);
            facility = base.Update(facility);
            Complete();
            return facility;
        }
    }
}
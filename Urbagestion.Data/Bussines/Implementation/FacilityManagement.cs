using System.Security.Principal;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Urbagestion.Model.Bussines.Common;
using Urbagestion.Model.Bussines.Interfaces;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;

namespace Urbagestion.Model.Bussines.Implementation
{
    public class FacilityManagement : BaseService<Facility>, IFacilityManagement
    {
        public FacilityManagement(IUnitOfWork unitOfWork, IPrincipal principal) : base(unitOfWork, principal)
        {
        }


        public Facility CreateFacility(Facility facility)
        {
            return Create(facility);
        }

        public Facility Create(Facility entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
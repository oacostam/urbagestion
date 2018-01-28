using System.Security.Principal;
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

        public override Facility Create(Facility entity)
        {
            
            return base.Create(entity);
        }
    }
}
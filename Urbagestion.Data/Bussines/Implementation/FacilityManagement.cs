using Urbagestion.Model.Bussines.Common;
using Urbagestion.Model.Bussines.Interfaces;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;

namespace Urbagestion.Model.Bussines.Implementation
{
    public class FacilityManagement : BaseService<Facility>, IFacilityManagement
    {
        public FacilityManagement(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        public Facility CreateFacility(Facility facility)
        {
            return Create(facility);
        }
    }
}
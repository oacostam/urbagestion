using Urbagestion.Model.Models;
using Urbagestion.Util;

namespace Urbagestion.Model.Bussines.Interfaces
{
    public interface IFacilityManagement
    {
        Facility[] GetAll(int page, int size, out int total, string orderBy, SortOrder sortOrder);
        Facility CreateFacility(Facility facility);
        Facility Create(Facility entity);
        void Delete(Facility entity);
        Facility Update(Facility entity);
        Facility GetById(int id);
        Facility SetActive(int id, bool active);
    }
}
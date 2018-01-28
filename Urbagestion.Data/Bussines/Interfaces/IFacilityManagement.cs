using Urbagestion.Model.Models;
using Urbagestion.Util;

namespace Urbagestion.Model.Bussines.Interfaces
{
    public interface IFacilityManagement
    {
        void Dispose();
        Facility Create(Facility entity);
        void Delete(Facility entity, bool logicalDelete = true);
        Facility[] GetAll(int page, int size, out int total, string orderBy, SortOrder sortOrder);
        Facility Update(Facility entity);
        Facility GetById(int id);
        void Dispose(bool disposing);
        void Complete();
    }
}
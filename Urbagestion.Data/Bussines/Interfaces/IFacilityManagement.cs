using System.Collections.Generic;
using Urbagestion.Model.Models;

namespace Urbagestion.Model.Bussines.Interfaces
{
    public interface IFacilityManagement
    {
        IEnumerable<Facility> GetAll(int page, int size, out int total);
        Facility CreateFacility(Facility facility);
        Facility Create(Facility entity);
        void Delete(Facility entity);
        Facility Update(Facility entity);
        Facility GetById(int id);
        Facility SetActive(int id, bool active);
    }
}
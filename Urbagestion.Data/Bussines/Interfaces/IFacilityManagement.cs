using System;
using Urbagestion.Model.Models;
using Urbagestion.Util;

namespace Urbagestion.Model.Bussines.Interfaces
{
    public interface IFacilityManagement : IDisposable
    {
        Facility Create(Facility entity);
        void Delete(int id);
        Facility Update(Facility facility);
        Facility[] GetAll(int page, int size, out int total, string orderBy, SortOrder sortOrder);
        Facility GetById(int id);
    }
}
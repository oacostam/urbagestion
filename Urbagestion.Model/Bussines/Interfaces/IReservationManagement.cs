using Urbagestion.Model.Models;

namespace Urbagestion.Model.Bussines.Interfaces
{
    public interface IReservationManagement
    {
        Reservation Add(Reservation reservation);
    }
}
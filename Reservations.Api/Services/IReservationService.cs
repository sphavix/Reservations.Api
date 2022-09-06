using Reservations.Api.Models;

namespace Reservations.Api.Services
{
    public interface IReservationService
    {
        List<Reservation> GetReservations();
        Reservation GetReservationById(int id);
        string AddReservation(Reservation reservation);
        string UpdateReservation(Reservation reservation);
        string DeleteReservation(int id);
    }
}

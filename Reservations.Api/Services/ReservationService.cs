using Reservations.Api.Models;
using Reservations.Api.Models.Database;

namespace Reservations.Api.Services
{
    public class ReservationService: IReservationService
    {
        private readonly ReservationsDbContext _contxt;
        public ReservationService(ReservationsDbContext contxt)
        {
            _contxt = contxt;
        }

        public List<Reservation> GetReservations()
        {
            return _contxt.Reservations.ToList();
        }

        public Reservation GetReservationById(int id)
        {
            return _contxt.Reservations.FirstOrDefault(x => x.Id == id);
        }

        public string AddReservation(Reservation model)
        {
            using(var transaction = _contxt.Database.BeginTransaction())
            {
                using (_contxt)
                {
                    _contxt.Reservations.Add(model);
                    _contxt.SaveChanges();
                    transaction.Commit();
                }
                return "You have successfully made a reservation!";
            }
        }

        public string UpdateReservation(Reservation model)
        {
            using (var transaction = _contxt.Database.BeginTransaction())
            {
                using (_contxt)
                {
                    var reservation = _contxt.Reservations.FirstOrDefault(x => x.Id == model.Id);
                    if(reservation != null)
                    {
                        reservation.Id = model.Id;
                        reservation.Name = model.Name;
                        reservation.StartLocation = model.StartLocation;
                        reservation.EndLocation = model.EndLocation;

                        _contxt.Reservations.Update(model);
                        _contxt.SaveChanges();
                        transaction.Commit();
                    }
                }
                return "You have successfully updated a reservation!";
            }
        }

        public string DeleteReservation(int id)
        {
            using (var transaction = _contxt.Database.BeginTransaction())
            {
                using (_contxt)
                {
                    var reservation = _contxt.Reservations.FirstOrDefault(x => x.Id == id);
                    if(reservation != null)
                    {
                        _contxt.Reservations.Remove(reservation);
                        _contxt.SaveChanges();
                        transaction.Commit();
                    }
                }
                return "You have successfully deleted a reservation!";
            }
        }
    }
}

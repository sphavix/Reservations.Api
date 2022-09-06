using Microsoft.EntityFrameworkCore;

namespace Reservations.Api.Models.Database
{
    public class ReservationsDbContext:DbContext
    {
        public ReservationsDbContext(DbContextOptions<ReservationsDbContext> options) : base(options)
        {

        }

        public DbSet<Reservation> Reservations { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace AirlineV2._5.Models
{
    public class AirlineV2Context : DbContext
    {
        public AirlineV2Context(DbContextOptions<AirlineV2Context> options) : base(options) { }

        public DbSet<Aircraft> Aircrafts { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<FlightAssignedTo> FlightAssignedEmps { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<ClnRelFlg> ClnFlgRels { get; set; }
    }
}

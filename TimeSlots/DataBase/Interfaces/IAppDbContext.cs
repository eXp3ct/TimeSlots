using Microsoft.EntityFrameworkCore;
using TimeSlots.Model;

namespace TimeSlots.DataBase.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Timeslot> Timeslots { get; set; }
        public DbSet<Gate> Gates { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<GateSchedule> GateSchedules { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<PlatformFavorite> PlatformFavorites { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

	}
}

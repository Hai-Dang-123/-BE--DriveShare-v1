using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implement
{
    public class VehicleBookingReportRepository : GenericRepository<VehicleBookingReport>, IVehicleBookingReportRepository
    {
        private readonly DriverShareAppContext _context;
        public VehicleBookingReportRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<VehicleBookingReport>> GetAllByBookingIdAsync(Guid vehicleBookingId)
        {
            return await _context.VehicleBookingReports
                .Where(r => r.VehicleBookingId == vehicleBookingId)
                .ToListAsync();
        }

    }
}

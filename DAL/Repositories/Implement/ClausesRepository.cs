using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implement
{
<<<<<<< Updated upstream:DAL/Repositories/Implement/ClausesRepository.cs
    public class ClausesRepository : GenericRepository<Clause>, IClausesRepository
    {
        private readonly DriverShareAppContext _context;
        public ClausesRepository(DriverShareAppContext context) : base(context)
=======
    public class VehicleBookingReportRepository : GenericRepository<VehicleBookingReport>, IVehicleBookingReportRepository
    {
        private readonly DriverShareAppContext _context;

        public VehicleBookingReportRepository(DriverShareAppContext context) : base(context)
>>>>>>> Stashed changes:DAL/Repositories/Implement/VehicleBookingRepository.cs
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

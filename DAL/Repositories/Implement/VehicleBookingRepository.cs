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
    public class VehicleBookingRepository : GenericRepository<VehicleBooking>, IVehicleBookingRepository
    {
        private readonly DriverShareAppContext _context;
        public VehicleBookingRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy Vehicle kèm PostVehicle và User liên quan.
        /// </summary>
        public async Task<VehicleBooking?> GetByIdIncludePostVehicleAsync(Guid bookingId)
        {
            return await _context.VehicleBookings
                .Include(ib => ib.PostVehicle)
                   
                .FirstOrDefaultAsync(ib => ib.VehicleBookingId == bookingId);
        }
    }
}

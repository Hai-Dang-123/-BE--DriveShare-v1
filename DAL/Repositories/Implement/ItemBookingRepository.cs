using Common.Enums;
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
    public class ItemBookingRepository : GenericRepository<ItemBooking>, IItemBookingRepository
    {
        private readonly DriverShareAppContext _context;
        public ItemBookingRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy ItemBooking kèm PostItem và User liên quan.
        /// </summary>
        public async Task<ItemBooking?> GetByIdIncludePostItemAsync(Guid bookingId)
        {
            return await _context.ItemBookings
                .Include(ib => ib.PostItem)
                   
                .FirstOrDefaultAsync(ib => ib.ItemBookingId == bookingId);
        }


        

    }
}

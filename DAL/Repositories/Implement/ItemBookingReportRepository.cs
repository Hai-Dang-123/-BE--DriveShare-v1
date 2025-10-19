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
    public class ItemBookingReportRepository : GenericRepository<ItemBookingReport>, IItemBookingReportRepository
    {
        private readonly DriverShareAppContext _context;

        public ItemBookingReportRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ItemBookingReport>> GetAllByBookingIdAsync(Guid itemBookingId)
        {
            return await _context.ItemBookingReports
                .Where(r => r.ItemBookingId == itemBookingId)
                .ToListAsync();
        }

    }
}

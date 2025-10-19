using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interface;

namespace DAL.Repositories.Implement
{
    public class ItemBookingReportRepository : GenericRepository<ItemBookingReport>, IItemBookingReportRepository
    {
        private readonly DriverShareAppContext _context;

        public ItemBookingReportRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }
    }
}

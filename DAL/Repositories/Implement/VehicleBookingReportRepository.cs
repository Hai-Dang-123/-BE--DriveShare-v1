using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interface;

namespace DAL.Repositories.Implement
{
    public class VehicleBookingReportRepository
        : GenericRepository<VehicleBookingReport>, IVehicleBookingReportRepository
    {
        public VehicleBookingReportRepository(DriverShareAppContext context) : base(context)
        {
        }
    }
}

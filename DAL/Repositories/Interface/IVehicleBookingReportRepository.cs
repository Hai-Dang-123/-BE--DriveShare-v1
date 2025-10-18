using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IVehicleBookingReportRepository : IGenericRepository<VehicleBookingReport>
    {
        Task<IEnumerable<VehicleBookingReport>> GetAllByBookingIdAsync(Guid vehicleBookingId);
    }
}

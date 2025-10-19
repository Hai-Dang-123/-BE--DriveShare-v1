using Common.Enums;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{

    // Create VehicleBookingService 
    // - VehicleBookingId
    // - PostVehicleId
    // - RenterUserId ( UserUtility lấy từ token )
    // - RentalStartDate
    // - RentalEndDate
    // - TotalPrice ( tính từ PostVehicle.PricePerDay * số ngày thuê )
    // - Status ( Mặc định là PENDING )
    // - CreatedAt ( mặc định DateTime.UtcNow )

    // => ResponseDTO ( result : VehicleBookingId )

    public class VehicleBookingService
    {
    }
}

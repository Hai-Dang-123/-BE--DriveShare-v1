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
    // - VehicleBookingId ( new Guid )
    // - PostVehicleId ( DTO truyền vào )
    // - RenterUserId ( UserUtility lấy từ token )
    // - RentalStartDate ( từ DTO truyền vào )
    // - RentalEndDate ( từ DTO truyền vào )
    // - TotalPrice ( tính từ PostVehicle.PricePerDay * số ngày thuê )
    // - Status ( Mặc định là PENDING )
    // - CreatedAt ( mặc định DateTime.UtcNow )

    // => ResponseDTO ( result : VehicleBookingId )

    // --------------------------------------------------

    // Change Status ( nhận VehicleBookingId và Status )

    // --------------------------------------------------

    // nếu có thời gian thì làm thêm:
    // Get VehicleBooking by RenterUserId ( lấy danh sách các booking của user này )
    // Get VehicleBooking by VehicleBookingId ( lấy chi tiết booking )


    public class VehicleBookingService
    {
    }
}

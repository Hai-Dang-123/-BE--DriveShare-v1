using Common.Enums;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{

    // Create ItemBookingService 
    // - ItemBookingId (Guid)
    // - PostItemId (Guid) -> PostItem
    // - DriverId (UserUtility -> UserId trong token )
    // - TotalPrice (decimal)
    // - Status (BookingStatus)
    // - CreatedAt (DateTime)
    // - PickupDate (DateTime?)
    // - DeliveryDate ( null )

    // => ResponseDTO ( result : ItemBookingId )

    // --------------------------------------------------

    // Change Status ( nhận ItemBookingId và Status )
    // nếu có thời gian thì làm thêm:
    // Get ItemBooking by RenterUserId ( lấy danh sách các booking của user này )
    // Get ItemBooking by VehicleBookingId ( lấy chi tiết booking )

    public class ItemBookingService
    {
    }
}

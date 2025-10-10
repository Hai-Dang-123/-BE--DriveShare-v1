using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ItemBookingReport : BaseReport
    {
        public Guid ItemBookingId { get; set; }
        public ItemBooking ItemBooking { get; set; } = null!;
    }
}

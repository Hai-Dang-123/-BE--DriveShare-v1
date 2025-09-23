using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implement
{
    public class PostVehicleRepository : GenericRepository<PostVehicle>, IPostVehicleRepository
    {
        private readonly DriverShareAppContext _context;
        public PostVehicleRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }
    }
}

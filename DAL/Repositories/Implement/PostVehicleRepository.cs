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
    public class PostVehicleRepository : GenericRepository<PostVehicle>, IPostVehicleRepository
    {
        private readonly DriverShareAppContext _context;
        public PostVehicleRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostVehicle>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.PostVehicles
                .Where(v => v.OwnerId == userId && v.Status != PostStatus.DELETED)
                .ToListAsync();
        }
    }
}

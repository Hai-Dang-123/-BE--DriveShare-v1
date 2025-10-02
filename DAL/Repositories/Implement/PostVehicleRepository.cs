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

        public async Task<IEnumerable<PostVehicle>> GetAllByPostAsync()
        {
            return await _context.PostVehicles
               .Where(v => v.Status != PostStatus.DELETED)
               .Include(v => v.Vehicle)
                   .ThenInclude(v => v.VehicleType)
               .Include(v => v.Owner)
               .Include(v => v.Clause)
               .ToListAsync();
        }

        public async Task<IEnumerable<PostVehicle>> GetAllByStatusAsync(PostStatus status)
        {
            return await _context.Set<PostVehicle>()
                .Include(p => p.Owner)
                .Include(p => p.Clause)
                .Include(p => p.Vehicle)
                    .ThenInclude(v => v.VehicleType)
                .Where(p => p.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<PostVehicle>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.PostVehicles
                .Where(v => v.OwnerId == userId && v.Status != PostStatus.DELETED)
                .Include(v => v.Vehicle)
                    .ThenInclude(v => v.VehicleType)
                .Include(v => v.Owner)
                .Include(v => v.Clause)
                .ToListAsync();
        }

        public async Task<PostVehicle?> GetPostByIdAsync(Guid postId)
        {
            return await _context.PostVehicles
                .Where(v => v.PostVehicleId == postId && v.Status != PostStatus.DELETED)
                .Include(v => v.Vehicle)
                    .ThenInclude(v => v.VehicleType)
                .Include(v => v.Owner)
                .Include(v => v.Clause)  
                .FirstOrDefaultAsync();
        }

    }
}

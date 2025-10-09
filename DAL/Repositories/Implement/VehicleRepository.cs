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
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly DriverShareAppContext _context;
        public VehicleRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Vehicle?> FindByLicenseAsync(string plateNumber)
        {
            return await _context.Vehicles
                .FirstOrDefaultAsync(v => v.PlateNumber == plateNumber);
        }

        public async Task<IEnumerable<Vehicle>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Vehicles
                .Where(v => v.UserId == userId && v.Status != VehicleStatus.DELETED)
                .ToListAsync();
        }
        public async Task<IEnumerable<Vehicle>> GetAllWithImagesByUserIdAsync(Guid userId)
        {
            return await _context.Vehicles
                .Include(v => v.VehicleImages)
                .Include(v => v.VehicleType)
                .Where(v => v.UserId == userId && v.Status != VehicleStatus.DELETED)
                .ToListAsync();
        }
        public async Task<Vehicle?> GetByIdWithImagesAsync(Guid id)
        {
            return await _context.Vehicles
                .Include(v => v.VehicleImages)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(v => v.VehicleId == id);
        }

    }
}

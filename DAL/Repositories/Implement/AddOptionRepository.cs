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
    public class AddOptionRepository : GenericRepository<AddOption>, IAddOptionRepository
    {
        private readonly DriverShareAppContext _context;

        public AddOptionRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AddOption>> GetAllByPostVehicleIdAsync(Guid postVehicleId)
        {
            return await _context.AddOptions
                .Where(a => a.PostVehicleId == postVehicleId)
                .ToListAsync();
        }
    }
}

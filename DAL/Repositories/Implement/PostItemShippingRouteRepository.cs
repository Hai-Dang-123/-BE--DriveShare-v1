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
    public class PostItemShippingRouteRepository : GenericRepository<PostItemShippingRoute>, IPostItemShippingRouteRepository
    {
        public PostItemShippingRouteRepository(DriverShareAppContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PostItemShippingRoute>> GetAllAsync()
        {
            return await _context.PostItemShippingRoutes.ToListAsync(); 
        }
    }
}

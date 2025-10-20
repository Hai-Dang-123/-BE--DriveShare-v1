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
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly DriverShareAppContext _context;
        public ReviewRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllCReviewAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByToUserIdAsync(Guid userId)
        {
            return await _context.Reviews
               .Where(r => r.ToUserId == userId)
               .AsNoTracking()
               .ToListAsync();
        }
    }
}

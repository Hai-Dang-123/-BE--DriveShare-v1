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

    public class PostItemRepository : GenericRepository<PostItem>, IPostItemRepository
    {
        private readonly DriverShareAppContext _context;
        public PostItemRepository(DriverShareAppContext context) : base(context)

        {
            _context = context;
        }

        public async Task<IEnumerable<PostItem>> GetAllPostItemsAsync()
        {
            return await _context.PostItems.ToListAsync();
        }

        public async Task<PostItem> GetItemByIdAsync(Guid id)
        {
           return await _context.PostItems
              .Include(p => p.Item)
                .ThenInclude(i => i.Characteristics)
              .Include(p => p.Route)
              .FirstOrDefaultAsync(p => p.PostItemId == id);

        }
    }
}

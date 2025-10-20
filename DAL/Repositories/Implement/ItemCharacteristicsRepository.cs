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
    public class ItemCharacteristicsRepository : GenericRepository<ItemCharacteristics>, IItemCharacteristicsRepository
    {
        public ItemCharacteristicsRepository(DriverShareAppContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ItemCharacteristics>> GetAllItemCharacteristicsAsync()
        {
            return await _context.ItemCharacteristics.ToListAsync();
        }
    }
}

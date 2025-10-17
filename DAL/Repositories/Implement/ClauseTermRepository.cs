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
    public class ClauseTermRepository : GenericRepository<ClauseTerm>, IClauseTermRepository
    {
        private readonly DriverShareAppContext _context;
        public ClauseTermRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClauseTerm>> GetAllClauseTerm()
        {
            return await _context.ClauseContents.ToListAsync();
        }
    }
}

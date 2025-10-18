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
    public class ClauseTemplateRepository : GenericRepository<ClauseTemplate>, IClauseTemplateRepository
    {
        private readonly DriverShareAppContext _context;
        public ClauseTemplateRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClauseTemplate>> GetAllWithTermsAsync()
        {
            return await _context.ClauseTemplates
                .Include(x => x.Terms)
                .ToListAsync();
        }


        public async Task<ClauseTemplate> GetClauseWithTermsAsync(Guid clauseId)
        {
            return await _context.ClauseTemplates
                .Include(x => x.Terms)
                .FirstOrDefaultAsync(x => x.ClauseId == clauseId);
        }
    }
}

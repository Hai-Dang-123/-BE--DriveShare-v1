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
    public class ContractTemplateRepository : GenericRepository<ContractTemplate>, IContractTemplateRepository
    {
        public readonly DriverShareAppContext _context;
        public ContractTemplateRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContractTemplate>> GetAllWithTermsAsync()
        {
            return await _context.ContractTemplates
                .Include(ct => ct.ContractTerms)
                .ToListAsync();
        }

        public Task<ContractTemplate?> GetByIdWithTermsAsync(Guid id)
        {
            return _context.ContractTemplates
                .Include(ct => ct.ContractTerms)
                .FirstOrDefaultAsync(ct => ct.ContractTemplateId == id);
        }
    }
}

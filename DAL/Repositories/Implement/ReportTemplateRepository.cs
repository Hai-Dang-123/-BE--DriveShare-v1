using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implement
{
    public class ReportTemplateRepository : GenericRepository<ReportTemplate>, IReportTemplateRepository
    {
        public ReportTemplateRepository(DriverShareAppContext context) : base(context) { }
    
    public async Task<ReportTemplate?> GetByIdWithTermsAsync(Guid id)
        {
            return await _context.ReportTemplates
                .Include(rt => rt.ReportTerms)
                .FirstOrDefaultAsync(rt => rt.ReportTemplateId == id);
        }
        public async Task<List<ReportTemplate>> GetAllWithTermsAsync()
        {
            return await _context.ReportTemplates
                .Include(rt => rt.ReportTerms)
                .ToListAsync();
        }
    }
}
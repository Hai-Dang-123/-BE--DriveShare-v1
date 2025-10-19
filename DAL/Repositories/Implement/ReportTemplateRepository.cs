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
        // 🟢 Tạo mới ReportTemplate
        public async Task<ReportTemplate> CreateReportTemplateAsync(ReportTemplate template)
        {
            _context.ReportTemplates.Add(template);
            await _context.SaveChangesAsync();
            return template;
        }

        // 🟢 Cập nhật ReportTemplate
        public async Task<ReportTemplate?> UpdateReportTemplateAsync(Guid id, ReportTemplate updatedTemplate)
        {
            var existing = await _context.ReportTemplates
                .Include(rt => rt.ReportTerms)
                .FirstOrDefaultAsync(rt => rt.ReportTemplateId == id);

            if (existing == null)
                return null;

            existing.Version = updatedTemplate.Version;

            // Xóa term cũ
            _context.ReportTerms.RemoveRange(existing.ReportTerms);

            // Thêm lại term mới
            if (updatedTemplate.ReportTerms != null)
            {
                foreach (var term in updatedTemplate.ReportTerms)
                {
                    existing.ReportTerms.Add(new ReportTerm
                    {
                        ReportTermId = Guid.NewGuid(),
                        Content = term.Content,
                        IsMandatory = term.IsMandatory,
                        ReportTemplateId = existing.ReportTemplateId
                    });
                }
            }

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
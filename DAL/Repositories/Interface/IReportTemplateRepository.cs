using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IReportTemplateRepository : IGenericRepository<ReportTemplate>
    {
        Task<ReportTemplate?> GetByIdWithTermsAsync(Guid id);
        Task<List<ReportTemplate>> GetAllWithTermsAsync();
        Task<ReportTemplate> CreateReportTemplateAsync(ReportTemplate template);
        Task<ReportTemplate?> UpdateReportTemplateAsync(Guid id, ReportTemplate template);
    }
}

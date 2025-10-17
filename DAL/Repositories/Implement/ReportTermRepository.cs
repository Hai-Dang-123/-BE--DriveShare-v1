using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interface;

namespace DAL.Repositories.Implement
{
    public class ReportTermRepository : GenericRepository<ReportTerm>, IReportTermRepository
    {
        public ReportTermRepository(DriverShareAppContext context) : base(context) { }
    }
}

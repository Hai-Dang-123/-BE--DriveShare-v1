using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interface;

namespace DAL.Repositories.Implement
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(DriverShareAppContext context) : base(context) { }
    }
}

using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IClauseTemplateRepository : IGenericRepository<ClauseTemplate>
    {
        Task<ClauseTemplate> GetClauseWithTermsAsync(Guid clauseId);

        Task<IEnumerable<ClauseTemplate>> GetAllWithTermsAsync();

    }
}

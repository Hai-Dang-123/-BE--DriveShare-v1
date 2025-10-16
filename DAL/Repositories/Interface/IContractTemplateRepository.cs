using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IContractTemplateRepository : IGenericRepository<ContractTemplate>
    {
         Task<ContractTemplate?> GetByIdWithTermsAsync(Guid id);
        Task<IEnumerable<ContractTemplate>> GetAllWithTermsAsync();

        
    }
}

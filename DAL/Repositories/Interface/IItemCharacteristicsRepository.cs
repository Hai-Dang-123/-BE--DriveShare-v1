using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IItemCharacteristicsRepository : IGenericRepository<ItemCharacteristics>
    {
        Task<IEnumerable<ItemCharacteristics>> GetAllItemCharacteristicsAsync();
    }
}

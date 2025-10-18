using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IAddOptionRepository : IGenericRepository<AddOption>
    {
        Task<IEnumerable<AddOption>> GetAllByPostVehicleIdAsync(Guid postVehicleId);
    }
}

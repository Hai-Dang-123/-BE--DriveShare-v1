using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IPostVehicleRepository : IGenericRepository<PostVehicle>
    {
        Task<IEnumerable<PostVehicle>> GetAllByUserIdAsync(Guid userId);
      
        Task<PostVehicle> GetPostByIdAsync(Guid PostId);
    }
}

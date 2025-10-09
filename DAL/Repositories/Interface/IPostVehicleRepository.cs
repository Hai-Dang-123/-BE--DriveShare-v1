using Common.Enums;
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

        Task<IEnumerable<PostVehicle>> GetAllByPostAsync();

        Task<PostVehicle> GetPostByIdAsync(Guid PostId);

        Task<IEnumerable<PostVehicle>> GetAllByStatusAsync(PostStatus status);
    }
}

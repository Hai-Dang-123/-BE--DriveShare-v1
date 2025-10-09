using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
        Task<Vehicle?> FindByLicenseAsync(string plateNumber);
        Task<IEnumerable<Vehicle>> GetAllByUserIdAsync(Guid userId);
        Task<IEnumerable<Vehicle>> GetAllWithImagesByUserIdAsync(Guid userId);
        Task<Vehicle?> GetByIdWithImagesAsync(Guid id);
    }
}

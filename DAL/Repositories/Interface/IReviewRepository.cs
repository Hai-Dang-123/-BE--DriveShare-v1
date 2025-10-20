using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<IEnumerable<Review>> GetAllCReviewAsync();

        Task<IEnumerable<Review>> GetReviewsByToUserIdAsync(Guid userId);
    }
}

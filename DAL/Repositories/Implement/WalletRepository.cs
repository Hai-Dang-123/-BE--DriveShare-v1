using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implement
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {
        private readonly DriverShareAppContext _context;
        public WalletRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }

        // GetByUserIdAsync
        public async Task<Wallet> GetByUserIdAsync(Guid userId)
        {
            return await Task.FromResult(_context.Wallets.FirstOrDefault(w => w.UserId == userId));
        }
    }
}

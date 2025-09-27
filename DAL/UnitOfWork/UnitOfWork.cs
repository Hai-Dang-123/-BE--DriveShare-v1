using DAL.Context;
using DAL.Repositories.Implement;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DriverShareAppContext _context;
        private bool _disposed = false;
        private IDbContextTransaction _transaction;

        // Lazy repositories
        private IBookingRepository _bookingRepo;
        private IContractRepository _contractRepo;
        private IContractTermRepository _contractTermRepo;
        private INotificationRepository _notificationRepo;
        private IReviewRepository _reviewRepo;
        private IRoleRepository _roleRepo;
        private ITripRepository _tripRepo;
        private ITripDriverRepository _tripDriverRepo;
        private ITripStepInPlanRepository _tripStepInPlanRepo;
        private IRuleRepository _ruleRepo;
        private IUserRepository _userRepo;
        private IUserTokenRepository _userTokenRepo;
        private IVerificationRepository _verificationRepo;
        private IWalletRepository _walletRepo;
        private ITransactionRepository _transactionRepo;
        private IVehicleRepository _vehicleRepo;
        private IVehicleTypeRepository _vehicleTypeRepo;
        private IVehicleImagesRepository _vehicleImagesRepo;
        private IPostVehicleRepository _postVehicleRepo;

        public UnitOfWork(DriverShareAppContext context)
        {
            _context = context;
        }

        // Repository properties
        public IBookingRepository BookingRepo => _bookingRepo ??= new BookingRepository(_context);
        public IContractRepository ContractRepo => _contractRepo ??= new ContractRepository(_context);
        public IContractTermRepository ContractTermRepo => _contractTermRepo ??= new ContractTermRepository(_context);
        public INotificationRepository NotificationRepo => _notificationRepo ??= new NotificationRepository(_context);
        public IReviewRepository ReviewRepo => _reviewRepo ??= new ReviewRepository(_context);
        public IRoleRepository RoleRepo => _roleRepo ??= new RoleRepository(_context);
        public ITripRepository TripRepo => _tripRepo ??= new TripRepository(_context);
        public ITripDriverRepository TripDriverRepo => _tripDriverRepo ??= new TripDriverRepository(_context);
        public ITripStepInPlanRepository TripStepInPlanRepo => _tripStepInPlanRepo ??= new TripStepInPlanRepository(_context);
        public IRuleRepository RuleRepo => _ruleRepo ??= new RuleRepository(_context);
        public IUserRepository UserRepo => _userRepo ??= new UserRepository(_context);
        public IUserTokenRepository UserTokenRepo => _userTokenRepo ??= new UserTokenRepository(_context);
        public IVerificationRepository VerificationRepo => _verificationRepo ??= new VerificationRepository(_context);
        public IWalletRepository WalletRepo => _walletRepo ??= new WalletRepository(_context);
        public ITransactionRepository TransactionRepo => _transactionRepo ??= new TransactionRepository(_context);
        public IVehicleRepository VehicleRepo => _vehicleRepo ??= new VehicleRepository(_context);
        public IVehicleTypeRepository VehicleTypeRepo => _vehicleTypeRepo ??= new VehicleTypeRepository(_context);
        public IVehicleImagesRepository VehicleImagesRepo => _vehicleImagesRepo ??= new VehicleImagesRepository(_context);
        public IPostVehicleRepository PostVehicleRepo => _postVehicleRepo ??= new PostVehicleRepository(_context);

        // Transaction methods
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    await _transaction.CommitAsync();
                }
                catch
                {
                    await RollbackTransactionAsync();
                    throw;
                }
                finally
                {
                    await _transaction.DisposeAsync();
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        // Save changes
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await SaveAsync() > 0;
        }

        // Dispose pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

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
        private IDbContextTransaction? _transaction;

        

        public UnitOfWork(DriverShareAppContext context)
        {
            _context = context;

            //BookingRepo = new BookingRepository(_context);
            ContractRepo = new ContractRepository(_context);
            ContractTermRepo = new ContractTermRepository(_context);
            NotificationRepo = new NotificationRepository(_context);
            ReviewRepo = new ReviewRepository(_context);
            RoleRepo = new RoleRepository(_context);
            TripRepo = new TripRepository(_context);
            TripDriverRepo = new TripDriverRepository(_context);
            TripStepInPlanRepo = new TripStepInPlanRepository(_context);
            RuleRepo = new RuleRepository(_context);
            UserRepo = new UserRepository(_context);
            UserTokenRepo = new UserTokenRepository(_context);
            VerificationRepo = new VerificationRepository(_context);
            WalletRepo = new WalletRepository(_context);
            TransactionRepo = new TransactionRepository(_context);
            VehicleRepo = new VehicleRepository(_context);
            VehicleTypeRepo = new VehicleTypeRepository(_context);
            VehicleImagesRepo = new VehicleImagesRepository(_context);
            PostVehicleRepo = new PostVehicleRepository(_context);
            ClausesRepo = new ClausesRepository(_context);

            VehicleBookingRepo = new VehicleBookingRepository(_context);
            ItemBookingRepo = new ItemBookingRepository(_context);

        }

        

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


        //public IBookingRepository BookingRepo { get; private set; } 
        public IVehicleBookingRepository VehicleBookingRepo { get; private set; }
        public IItemBookingRepository ItemBookingRepo { get; private set; }
        public IContractRepository ContractRepo { get; private set; }
        public IContractTermRepository ContractTermRepo { get; private set; }
        public INotificationRepository NotificationRepo { get; private set; }
        public IReviewRepository ReviewRepo { get; private set; }
        public IRoleRepository RoleRepo { get; private set; }
        public ITripRepository TripRepo { get; private set; }
        public ITripDriverRepository TripDriverRepo { get; private set; }
        public ITripStepInPlanRepository TripStepInPlanRepo { get; private set; }
        public IRuleRepository RuleRepo { get; private set; }
        public IUserRepository UserRepo { get; private set; }
        public IUserTokenRepository UserTokenRepo { get; private set; }
        public IVerificationRepository VerificationRepo { get; private set; }
        public IWalletRepository WalletRepo { get; private set; }
        public ITransactionRepository TransactionRepo { get; private set; }
        public IVehicleRepository VehicleRepo { get; private set; }
        public IVehicleTypeRepository VehicleTypeRepo { get; private set; }
        public IVehicleImagesRepository VehicleImagesRepo { get; private set; }
        public IPostVehicleRepository PostVehicleRepo { get; private set; }
        public IClausesRepository ClausesRepo { get; private set; }

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

using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository BookingRepo { get; }
        IContractRepository ContractRepo { get; }
        IContractTermRepository ContractTermRepo { get; }
        INotificationRepository NotificationRepo { get; }
        IPostVehicleRepository PostVehicleRepo { get; }
        IReviewRepository ReviewRepo { get; }
        IRoleRepository RoleRepo { get; }
        IRuleRepository RuleRepo { get; }
        ITransactionRepository TransactionRepo { get; }
        ITripRepository TripRepo { get; }
        ITripDriverRepository TripDriverRepo { get; }
        ITripStepInPlanRepository TripStepInPlanRepo { get; }
        IUserRepository UserRepo { get; }
        IUserTokenRepository UserTokenRepo { get; }
        IVerificationRepository VerificationRepo { get; }
        IVehicleImagesRepository VehicleImagesRepo { get; }
        IVehicleRepository VehicleRepo { get; }
        IVehicleTypeRepository VehicleTypeRepo { get; }
        IWalletRepository WalletRepo { get; }
        Task<int> SaveAsync();
        Task<bool> SaveChangeAsync();
    }
}

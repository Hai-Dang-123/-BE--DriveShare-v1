using DAL.Repositories.Interface;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {


        IAddOptionRepository AddOptionRepo { get; }
        IVehicleBookingRepository VehicleBookingRepo { get; }
        IItemBookingRepository ItemBookingRepo { get; }

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
        IContractTemplateRepository ContractTemplateRepo { get; }
        IClauseTemplateRepository ClauseTemplateRepo { get; }

        IVehicleBookingReportRepository VehicleBookingReportRepo { get; }

        IPostItemRepository PostItemRepo { get; }


        IClauseTermRepository ClauseTermRepo { get; }
        IVehicleContractRepository VehicleContractRepo { get; }
        IItemContractRepository ItemContractRepo { get; }

        IReportRepository ReportRepo { get; }
        IReportTermRepository ReportTermRepo { get; }
        IReportTemplateRepository ReportTemplateRepo { get; }
        IItemBookingReportRepository ItemBookingReportRepo { get; }


        IItemBookingReportRepository ItemBookingReportRepo { get; }


        // Save changes

        Task<int> SaveAsync();
        Task<bool> SaveChangeAsync();

        // Transaction methods
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}

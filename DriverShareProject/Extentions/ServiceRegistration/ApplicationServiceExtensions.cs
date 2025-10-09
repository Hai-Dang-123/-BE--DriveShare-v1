using BLL.Services.Implement;
using BLL.Services.Interface;
using BLL.Utilities;
using DAL.Context;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DriverShareProject.Extentions.ServiceRegistration
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddDbContext<DriverShareAppContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //Service
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IVehicleService, VehicleService>();

            //services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IContractTemplateService, ContractTemplateService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IFirebaseUploadService , FirebaseUploadService>();

            services.AddScoped<IClausesService, ClausesService>();
            services.AddScoped<IPostVehicleService, PostVehicleService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<UserUtility>();

            services.AddHttpClient<IVNPTTokenService, VNPTTokenService>();
            services.AddHttpClient<IEKYCService, EKYCService>(client =>
            {
                client.BaseAddress = new Uri(configuration["VNPT:BaseUrl"]);
                client.Timeout = TimeSpan.FromMinutes(5);
            });

            return services;
        }
    }
}

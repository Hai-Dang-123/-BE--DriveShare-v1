using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class DriverShareAppContext : DbContext
    {
        public DriverShareAppContext(DbContextOptions<DriverShareAppContext> options) : base(options) { }

        // ---------------------- User & Authentication ----------------------
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Verification> Verifications { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

        // ---------------------- Wallet & Transactions ----------------------
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        // ---------------------- Vehicle Management ----------------------
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<VehicleImages> VehicleImages { get; set; }
        public DbSet<PostVehicle> PostVehicles { get; set; }
        public DbSet<Clause> Clauses { get; set; }

        // ---------------------- Booking & Contracts ----------------------
        public DbSet<VehicleBooking> VehicleBookings { get; set; }
        public DbSet<ItemBooking> ItemBookings { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractTerm> ContractTerms { get; set; }
        public DbSet<ContractTemplate> ContractTemplates { get; set; }

        // ---------------------- Trip & Driver Management ----------------------
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripDriver> TripDrivers { get; set; }
        public DbSet<TripStepInPlan> TripStepInPlans { get; set; }

        // ---------------------- Items, Reports, Rules ----------------------
        public DbSet<PostItem> PostItems { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportTemplate> ReportTemplates { get; set; }
        public DbSet<ReportTerm> ReportTerms { get; set; }
        public DbSet<VehicleInspection> VehicleInspections { get; set; }
        public DbSet<Rule> Rules { get; set; }

        // ---------------------- User Activity, Notifications, Reviews ----------------------
        public DbSet<UserViolation> UserViolations { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------------------- Keys ----------------------
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<Wallet>().HasKey(w => w.WalletId);
            modelBuilder.Entity<Transaction>().HasKey(t => t.TransactionId);
            modelBuilder.Entity<Vehicle>().HasKey(v => v.VehicleId);
            modelBuilder.Entity<VehicleType>().HasKey(vt => vt.VehicleTypeId);
            modelBuilder.Entity<VehicleImages>().HasKey(vi => vi.VehicleImageId);
            modelBuilder.Entity<PostVehicle>().HasKey(pv => pv.PostVehicleId);
            modelBuilder.Entity<PostItem>().HasKey(pi => pi.PostItemId);
            modelBuilder.Entity<VehicleBooking>().HasKey(vb => vb.VehicleBookingId);
            modelBuilder.Entity<ItemBooking>().HasKey(ib => ib.ItemBookingId);
            modelBuilder.Entity<Contract>().HasKey(c => c.ContractId);
            modelBuilder.Entity<ContractTerm>().HasKey(ct => ct.ContractTermId);
            modelBuilder.Entity<ContractTemplate>().HasKey(ct => ct.ContractTemplateId);
            modelBuilder.Entity<Trip>().HasKey(t => t.TripId);
            modelBuilder.Entity<TripDriver>().HasKey(td => td.TripDriverId);
            modelBuilder.Entity<TripStepInPlan>().HasKey(tp => tp.TripStepInPlanId);
            modelBuilder.Entity<Verification>().HasKey(v => v.VerificationId);
            modelBuilder.Entity<UserToken>().HasKey(ut => ut.UserTokenId);
            modelBuilder.Entity<Notification>().HasKey(n => n.NotificationId);
            modelBuilder.Entity<Review>().HasKey(r => r.ReviewId);
            modelBuilder.Entity<Clause>().HasKey(c => c.ClauseId);
            modelBuilder.Entity<Report>().HasKey(r => r.ReportId);
            modelBuilder.Entity<ReportTemplate>().HasKey(rt => rt.ReportTemplateId);
            modelBuilder.Entity<ReportTerm>().HasKey(rt => rt.ReportTermId);
            modelBuilder.Entity<VehicleInspection>().HasKey(vi => vi.VehicleInspectionId);
            modelBuilder.Entity<UserViolation>().HasKey(uv => uv.UserViolationId);
            modelBuilder.Entity<UserActivityLog>().HasKey(ual => ual.UserActivityLogId);

            // ---------------------- Relationships ----------------------

            // User ↔ Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Vehicle ↔ User
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.User)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemBooking ↔ Driver (User)
            modelBuilder.Entity<ItemBooking>()
                .HasOne(ib => ib.Driver)
                .WithMany(u => u.ItemBookings)
                .HasForeignKey(ib => ib.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemBooking ↔ Vehicle
            modelBuilder.Entity<ItemBooking>()
                .HasOne(ib => ib.Vehicle)
                .WithMany()
                .HasForeignKey(ib => ib.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemBooking ↔ PostItem
            modelBuilder.Entity<ItemBooking>()
                .HasOne(ib => ib.PostItem)
                .WithMany()
                .HasForeignKey(ib => ib.PostItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // Contract ↔ ItemBooking (1-1)
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.ItemBooking)
                .WithOne(ib => ib.Contract)
                .HasForeignKey<Contract>(c => c.ItemBookingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Contract ↔ VehicleBooking (1-1)
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.VehicleBooking)
                .WithOne(vb => vb.Contract)
                .HasForeignKey<Contract>(c => c.VehicleBookingId)
                .OnDelete(DeleteBehavior.Restrict);

            // VehicleBooking ↔ User
            modelBuilder.Entity<VehicleBooking>()
                .HasOne(vb => vb.Renter)
                .WithMany(u => u.VehicleBookings)
                .HasForeignKey(vb => vb.RenterId)
                .OnDelete(DeleteBehavior.Restrict);

            // VehicleBooking ↔ PostVehicle
            modelBuilder.Entity<VehicleBooking>()
                .HasOne(vb => vb.PostVehicle)
                .WithMany()
                .HasForeignKey(vb => vb.PostVehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Wallet ↔ User (1-1)
            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Transaction ↔ Wallet
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId)
                .OnDelete(DeleteBehavior.Restrict);

            // Vehicle ↔ VehicleType
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleType)
                .WithMany(vt => vt.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Vehicle ↔ VehicleImages
            modelBuilder.Entity<VehicleImages>()
                .HasOne(vi => vi.Vehicle)
                .WithMany(v => v.VehicleImages)
                .HasForeignKey(vi => vi.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // PostVehicle ↔ Vehicle
            modelBuilder.Entity<PostVehicle>()
                .HasOne(pv => pv.Vehicle)
                .WithMany(v => v.Posts)
                .HasForeignKey(pv => pv.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // PostVehicle ↔ Owner (User)
            modelBuilder.Entity<PostVehicle>()
                .HasOne(pv => pv.Owner)
                .WithMany(u => u.PostVehicles)
                .HasForeignKey(pv => pv.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // PostVehicle ↔ Clause
            modelBuilder.Entity<PostVehicle>()
                .HasOne(pv => pv.Clause)
                .WithMany(c => c.Posts)
                .HasForeignKey(pv => pv.ClauseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Verification ↔ Vehicle
            modelBuilder.Entity<Verification>()
                .HasOne(v => v.Vehicle)
                .WithOne(vh => vh.Verification)
                .HasForeignKey<Verification>(v => v.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Verification ↔ User
            modelBuilder.Entity<Verification>()
                .HasOne(v => v.User)
                .WithMany(u => u.Verifications)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Report ↔ ItemBooking, VehicleBooking, ReportTemplate
            modelBuilder.Entity<Report>()
                .HasOne(r => r.ItemBooking)
                .WithMany(ib => ib.Reports)
                .HasForeignKey(r => r.ItemBookingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.VehicleBooking)
                .WithMany(vb => vb.Reports)
                .HasForeignKey(r => r.VehicleBookingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.ReportTemplate)
                .WithOne()
                .HasForeignKey<Report>(r => r.ReportTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.FromUser)
                .WithMany()
                .HasForeignKey(r => r.FromUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.ToUser)
                .WithMany()
                .HasForeignKey(r => r.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TripStepInPlan>()
                .HasOne(ts => ts.Trip)
                .WithMany(t => t.TripPlans)
                .HasForeignKey(ts => ts.TripId)
                .OnDelete(DeleteBehavior.Restrict); // 👈 KHÔNG dùng Cascade

            modelBuilder.Entity<TripStepInPlan>()
                .HasOne(ts => ts.Driver)
                .WithMany()
                .HasForeignKey(ts => ts.TripDriverId)
                .OnDelete(DeleteBehavior.Cascade); // 👈 Chỉ 1 bên Cascade là được

            //  UserToken
            modelBuilder.Entity<UserToken>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.Tokens)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Notification
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);



            // Precision setup
            modelBuilder.Entity<VehicleBooking>().Property(v => v.TotalPrice).HasPrecision(18, 2);
            modelBuilder.Entity<ItemBooking>().Property(i => i.TotalPrice).HasPrecision(18, 2);
            modelBuilder.Entity<PostVehicle>().Property(p => p.DailyPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Rule>().Property(r => r.RuleValue).HasPrecision(18, 2);
            modelBuilder.Entity<Transaction>().Property(t => t.Amount).HasPrecision(18, 2);
            modelBuilder.Entity<Wallet>().Property(w => w.Balance).HasPrecision(18, 2);
            

            modelBuilder.Entity<PostItem>(entity =>
            {
                entity.Property(e => e.TotalPrice).HasPrecision(18, 2);
                entity.Property(e => e.Volume).HasPrecision(10, 3);
                entity.Property(e => e.Weight).HasPrecision(10, 3);
            });


            // ---------------------- Seed Data ----------------------
            DbSeeder.Seed(modelBuilder);
        }
    }
}

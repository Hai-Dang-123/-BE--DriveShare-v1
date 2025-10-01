using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractTerm> ContractTerms { get; set; }

        // ---------------------- Trip & Driver Management ----------------------
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripDriver> TripDrivers { get; set; }
        public DbSet<TripStepInPlan> TripStepInPlans { get; set; }

        // ---------------------- Rules & Regulations ----------------------
        public DbSet<Rule> Rules { get; set; }

        // ---------------------- Notifications & Reviews ----------------------
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Hash key
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<Wallet>().HasKey(w => w.WalletId);
            modelBuilder.Entity<Transaction>().HasKey(t => t.TransactionId);
            modelBuilder.Entity<Vehicle>().HasKey(v => v.VehicleId);
            modelBuilder.Entity<VehicleType>().HasKey(vt => vt.VehicleTypeId);
            modelBuilder.Entity<VehicleImages>().HasKey(vi => vi.VehicleImageId);
            modelBuilder.Entity<PostVehicle>().HasKey(pv => pv.PostVehicleId);
            modelBuilder.Entity<Booking>().HasKey(b => b.BookingId);
            modelBuilder.Entity<Contract>().HasKey(c => c.ContractId);
            modelBuilder.Entity<ContractTerm>().HasKey(ct => ct.ContractTermId);
            modelBuilder.Entity<Trip>().HasKey(t => t.TripId);
            modelBuilder.Entity<TripDriver>().HasKey(td => td.TripDriverId);
            modelBuilder.Entity<TripStepInPlan>().HasKey(tp => tp.TripStepInPlanId);
            modelBuilder.Entity<Verification>().HasKey(uv => uv.VerificationId);
            modelBuilder.Entity<UserToken>().HasKey(ut => ut.UserTokenId);
            modelBuilder.Entity<Notification>().HasKey(n => n.NotificationId);
            modelBuilder.Entity<Review>().HasKey(r => r.ReviewId);
            modelBuilder.Entity<Rule>().HasKey(r => r.RuleId);
            
            
            // Relationships


            // Role & User 
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // User & Wallet
            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Wallet & Transaction
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId)
                .OnDelete(DeleteBehavior.Restrict);

            // Vehicle & VehicleType
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleType)
                .WithMany(vt => vt.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Vehicle & VehicleImages
            modelBuilder.Entity<VehicleImages>()
                .HasOne(vi => vi.Vehicle)
                .WithMany(v => v.VehicleImages)
                .HasForeignKey(vi => vi.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // PostVehicle & Vehicle
            modelBuilder.Entity<PostVehicle>()
                .HasOne(pv => pv.Vehicle)
                .WithMany(v => v.Posts)
                .HasForeignKey(pv => pv.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // PostVehicle & User (Owner)
            modelBuilder.Entity<PostVehicle>()
                .HasOne(pv => pv.Owner)
                .WithMany()
                .HasForeignKey(pv => pv.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            // PostVehicle & Clause 
            modelBuilder.Entity<PostVehicle>()
                .HasOne(pv => pv.Clause)
                .WithMany(c => c.Posts)
                .HasForeignKey(pv => pv.ClauseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking & PostVehicle
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.PostVehicle)
                .WithMany()
                .HasForeignKey(b => b.PostVehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking & Contract
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Booking)
                .WithMany(b => b.Contracts)
                .HasForeignKey(c => c.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Contract & ContractTerm
            modelBuilder.Entity<ContractTerm>()
                .HasOne(ct => ct.Contract)
                .WithMany(c => c.Terms)
                .HasForeignKey(ct => ct.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            // Trip & TripDriver
            modelBuilder.Entity<TripDriver>()
                .HasOne(td => td.Trip)
                .WithMany(t => t.Drivers)
                .HasForeignKey(td => td.TripId)
                .OnDelete(DeleteBehavior.Restrict);

            // TripDriver & User (optional)
            modelBuilder.Entity<TripDriver>()
                .HasOne(td => td.Driver)
                .WithMany()
                .HasForeignKey(td => td.DriverId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Trip & TripStepInPlan
            modelBuilder.Entity<TripStepInPlan>()
                .HasOne(tp => tp.Trip)
                .WithMany(t => t.TripPlans)
                .HasForeignKey(tp => tp.TripId)
                .OnDelete(DeleteBehavior.Restrict);


            // TripStepInPlan & TripDriver
            modelBuilder.Entity<TripStepInPlan>()
                .HasOne(tp => tp.Driver)
                .WithMany()
                .HasForeignKey(tp => tp.TripDriverId)
            .OnDelete(DeleteBehavior.Restrict);

            // User & Verification (1 - N)
            modelBuilder.Entity<Verification>()
                .HasOne(uv => uv.User)
                .WithMany(u => u.Verifications)
                .HasForeignKey(uv => uv.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //Vehicle & Verification (1 - 1)
            modelBuilder.Entity<Verification>()
                .HasOne(uv => uv.Vehicle)
                .WithOne(v => v.Verification)
                .HasForeignKey<Verification>(uv => uv.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

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

            // Review (FromUser/ToUser)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.FromUser)
                .WithMany()
                .HasForeignKey(r => r.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.ToUser)
                .WithMany()
                .HasForeignKey(r => r.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);


            // ContractTemplate ↔ ContractTerm (1-n)
            modelBuilder.Entity<ContractTemplate>()
                .HasMany(ct => ct.ContractTerm)
                .WithOne(t => t.ContractTemplate)
                .HasForeignKey(t => t.ContractTemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            // Optional: unique constraint để tránh 1 Term thuộc cả Template lẫn Contract cùng lúc
            modelBuilder.Entity<ContractTerm>()
                .HasCheckConstraint("CK_ContractTerm_Parent",
                    @"(ContractTemplateId IS NOT NULL AND ContractId IS NULL) 
              OR (ContractTemplateId IS NULL AND ContractId IS NOT NULL)");

            // Rule: chỉ cần mặc định

            modelBuilder.Entity<Booking>()
                .Property(b => b.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PostVehicle>()
                .Property(p => p.DailyPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Rule>()
                .Property(r => r.RuleValue)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Wallet>()
                .Property(w => w.Balance)
                .HasPrecision(18, 2);

            // Seed data if needed
            DbSeeder.Seed(modelBuilder);
        }
    }
}

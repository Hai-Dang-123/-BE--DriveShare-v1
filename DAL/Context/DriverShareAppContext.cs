using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using System.Reflection; // Để quét các lớp con nếu cần

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
        public DbSet<UserActivityLog> UserActivityLogs { get; set; } // Đã đưa lên đây để gần User
        public DbSet<UserViolation> UserViolations { get; set; } // Đã đưa lên đây để gần User


        // ---------------------- Wallet & Transactions ----------------------
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        // ---------------------- Vehicle Management ----------------------
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<VehicleImage> VehicleImages { get; set; } // Đổi từ VehicleImages
        public DbSet<AddOption> AddOptions { get; set; } // Entity AddOption cần DbSet
        public DbSet<PostVehicle> PostVehicles { get; set; }

        // ---------------------- Booking & Contracts ----------------------
        public DbSet<VehicleBooking> VehicleBookings { get; set; }
        public DbSet<ItemBooking> ItemBookings { get; set; }
        // DbSet cho BaseContract (sẽ được ánh xạ thành một bảng với các loại khác nhau)
        public DbSet<BaseContract> Contracts { get; set; }
        public DbSet<VehicleContract> VehicleContracts { get; set; } // Để dễ dàng truy vấn riêng
        public DbSet<ItemContract> ItemContracts { get; set; }     // Để dễ dàng truy vấn riêng
        public DbSet<ContractTemplate> ContractTemplates { get; set; }
        public DbSet<ContractTerm> ContractTerms { get; set; }



        // ---------------------- Trip & Driver Management ----------------------
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripDriver> TripDrivers { get; set; }
        public DbSet<TripStepInPlan> TripStepInPlans { get; set; }

        // ---------------------- Items, Reports, Rules ----------------------
        public DbSet<PostItem> PostItems { get; set; }
        // DbSet cho BaseReport
        public DbSet<BaseReport> Reports { get; set; }
        public DbSet<VehicleBookingReport> VehicleBookingReports { get; set; } // Để dễ dàng truy vấn riêng
        public DbSet<ItemBookingReport> ItemBookingReports { get; set; }     // Để dễ dàng truy vấn riêng
        public DbSet<ReportTemplate> ReportTemplates { get; set; }
        public DbSet<ReportTerm> ReportTerms { get; set; }

        public DbSet<VehicleInspection> VehicleInspections { get; set; }
        //public DbSet<InspectionResolution> InspectionResolutions { get; set; } // DbSet cho entity mới
        public DbSet<Rule> Rules { get; set; }
        public DbSet<ClauseTemplate> ClauseTemplates { get; set; } // Đổi từ Clauses
        public DbSet<ClauseContent> ClauseContents { get; set; } // DbSet cho entity mới


        // ---------------------- General ----------------------
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------------------- Key Configurations ----------------------
            // EF Core thường tự động nhận diện nếu các ID theo quy ước, nhưng liệt kê ra để rõ ràng
            modelBuilder.Entity<AddOption>().HasKey(ao => ao.AddOptionId);
            modelBuilder.Entity<ClauseTemplate>().HasKey(ct => ct.ClauseId);
            modelBuilder.Entity<ClauseContent>().HasKey(cc => cc.ClauseContentId);
            modelBuilder.Entity<ContractTemplate>().HasKey(ct => ct.ContractTemplateId);
            modelBuilder.Entity<ContractTerm>().HasKey(ct => ct.ContractTermId);
            modelBuilder.Entity<ContractTerm>().HasKey(cst => cst.ContractTermId);
            modelBuilder.Entity<ItemBooking>().HasKey(ib => ib.ItemBookingId);
            modelBuilder.Entity<Notification>().HasKey(n => n.NotificationId);
            modelBuilder.Entity<PostItem>().HasKey(pi => pi.PostItemId);
            modelBuilder.Entity<PostVehicle>().HasKey(pv => pv.PostVehicleId);
            modelBuilder.Entity<ReportTemplate>().HasKey(rt => rt.ReportTemplateId);
            modelBuilder.Entity<ReportTerm>().HasKey(rt => rt.ReportTermId);
            modelBuilder.Entity<ContractTerm>().HasKey(rst => rst.ContractTermId);
            modelBuilder.Entity<Review>().HasKey(r => r.ReviewId);
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<Rule>().HasKey(r => r.RuleId);
            modelBuilder.Entity<Transaction>().HasKey(t => t.TransactionId);
            modelBuilder.Entity<Trip>().HasKey(t => t.TripId);
            modelBuilder.Entity<TripDriver>().HasKey(td => td.TripDriverId);
            modelBuilder.Entity<TripStepInPlan>().HasKey(tsp => tsp.TripStepInPlanId);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<UserActivityLog>().HasKey(ual => ual.UserActivityLogId);
            modelBuilder.Entity<UserToken>().HasKey(ut => ut.UserTokenId);
            modelBuilder.Entity<UserViolation>().HasKey(uv => uv.UserViolationId);
            modelBuilder.Entity<Vehicle>().HasKey(v => v.VehicleId);
            modelBuilder.Entity<VehicleBooking>().HasKey(vb => vb.VehicleBookingId);
            modelBuilder.Entity<VehicleImage>().HasKey(vi => vi.VehicleImageId);
            modelBuilder.Entity<VehicleInspection>().HasKey(vi => vi.VehicleInspectionId);
            //modelBuilder.Entity<InspectionResolution>().HasKey(ir => ir.InspectionResolutionId);
            modelBuilder.Entity<VehicleType>().HasKey(vt => vt.VehicleTypeId);
            modelBuilder.Entity<Verification>().HasKey(v => v.VerificationId);
            modelBuilder.Entity<Wallet>().HasKey(w => w.WalletId);


            // ---------------------- TPH Inheritance for Contracts ----------------------
            // Contract base class mapping
            modelBuilder.Entity<BaseContract>()
                .HasKey(c => c.ContractId);
            modelBuilder.Entity<BaseContract>()
                .ToTable("Contracts") // All contracts in one table
                .HasDiscriminator<string>("ContractType") // Column to distinguish types
                .HasValue<VehicleContract>("VehicleContract")
                .HasValue<ItemContract>("ItemContract");

            // ---------------------- TPH Inheritance for Reports ----------------------
            // Report base class mapping
            modelBuilder.Entity<BaseReport>()
                .HasKey(r => r.ReportId);
            modelBuilder.Entity<BaseReport>()
                .ToTable("Reports") // All reports in one table
                .HasDiscriminator<string>("DiscriminatorType") // Column to distinguish types
                .HasValue<VehicleBookingReport>("VehicleBookingReport")
                .HasValue<ItemBookingReport>("ItemBookingReport");


            // ---------------------- Relationships ----------------------

            // User ↔ Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // User ↔ Wallet (1-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Nếu user bị xóa, ví cũng bị xóa

            // User ↔ Verification (User can have multiple verifications, verification processed by user)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Verifications)
                .WithOne(v => v.User)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Verification>()
                .HasOne(v => v.ProcessedByUser)
                .WithMany() // ProcessedByUser is not a dedicated collection in User
                .HasForeignKey(v => v.ProcessedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User ↔ UserToken
            modelBuilder.Entity<UserToken>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.Tokens)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa user thì token cũng xóa

            // User ↔ UserActivityLog
            modelBuilder.Entity<UserActivityLog>()
                .HasOne(ual => ual.User)
                .WithMany() // UserActivityLog không có collection ngược trong User
                .HasForeignKey(ual => ual.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa user thì log cũng xóa

            // User ↔ UserViolation
            modelBuilder.Entity<UserViolation>()
                .HasOne(uv => uv.User)
                .WithMany(u => u.UserViolations)
                .HasForeignKey(uv => uv.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa user thì vi phạm cũng xóa

            // User ↔ Notification
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa user thì thông báo cũng xóa

            // User ↔ Review (FromUser and ToUser)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.FromUser)
                .WithMany(u => u.ReviewsGiven)
                .HasForeignKey(r => r.FromUserId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa review nếu người viết bị xóa

            modelBuilder.Entity<Review>()
                .HasOne(r => r.ToUser)
                .WithMany(u => u.ReviewsReceived)
                .HasForeignKey(r => r.ToUserId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa review nếu người được đánh giá bị xóa


            // User ↔ PostVehicle (Owner)
            modelBuilder.Entity<PostVehicle>()
                .HasOne(pv => pv.Owner)
                .WithMany(u => u.CreatedPostVehicles)
                .HasForeignKey(pv => pv.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // User ↔ PostItem (Creator)
            modelBuilder.Entity<PostItem>()
                .HasOne(pi => pi.User)
                .WithMany(u => u.CreatedPostItems)
                .HasForeignKey(pi => pi.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User ↔ VehicleBooking (Renter)
            modelBuilder.Entity<VehicleBooking>()
                .HasOne(vb => vb.RenterUser)
                .WithMany(u => u.AsRenterBookings)
                .HasForeignKey(vb => vb.RenterUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User ↔ ItemBooking (DriverUser)
            modelBuilder.Entity<ItemBooking>()
                .HasOne(ib => ib.Driver)
                .WithMany(u => u.AsItemDriverBookings)
                .HasForeignKey(ib => ib.DriverId)
                .IsRequired(false) // DriverUser có thể null nếu là external driver
                .OnDelete(DeleteBehavior.Restrict);

            // User ↔ Trip (CreatorUser)
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.CreatorUser)
                .WithMany(u => u.CreatedTrips)
                .HasForeignKey(t => t.CreatorUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User ↔ TripDriver (DriverUser)
            modelBuilder.Entity<TripDriver>()
                .HasOne(td => td.DriverUser)
                .WithMany(u => u.AssignedTrips)
                .HasForeignKey(td => td.DriverUserId)
                .IsRequired(false) // DriverUser có thể null nếu là external driver
                .OnDelete(DeleteBehavior.Restrict);


            // Wallet ↔ Transaction
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

            // Vehicle ↔ User (OwnerUser)
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.OwnerUser)
                .WithMany(u => u.OwnedVehicles)
                .HasForeignKey(v => v.OwnerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Vehicle ↔ Verification (CurrentVerification)
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.CurrentVerification)
                .WithMany() // CurrentVerification không có collection ngược trong Verification
                .HasForeignKey(v => v.CurrentVerificationId)
                .IsRequired(false) // CurrentVerification có thể null
                .OnDelete(DeleteBehavior.Restrict);

            // Vehicle ↔ VehicleImage
            modelBuilder.Entity<VehicleImage>()
                .HasOne(vi => vi.Vehicle)
                .WithMany(v => v.Images)
                .HasForeignKey(vi => vi.VehicleId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa xe thì ảnh cũng xóa

            // PostVehicle ↔ Vehicle
            modelBuilder.Entity<PostVehicle>()
                .HasOne(pv => pv.Vehicle)
                .WithMany(v => v.PostsForRent)
                .HasForeignKey(pv => pv.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // PostVehicle ↔ ClauseTemplate
            modelBuilder.Entity<PostVehicle>()
                .HasOne(pv => pv.ClauseTemplate)
                .WithMany(ct => ct.Posts)
                .HasForeignKey(pv => pv.ClauseTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            // AddOption ↔ PostVehicle
            modelBuilder.Entity<AddOption>()
                .HasOne(ao => ao.PostVehicle)
                .WithMany(pv => pv.AddOptions)
                .HasForeignKey(ao => ao.PostVehicleId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa bài đăng thì tùy chọn cũng xóa

            // VehicleBooking ↔ PostVehicle
            modelBuilder.Entity<VehicleBooking>()
                .HasOne(vb => vb.PostVehicle)
                .WithMany(pv => pv.VehicleBookings)
                .HasForeignKey(vb => vb.PostVehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemBooking ↔ PostItem
            modelBuilder.Entity<ItemBooking>()
                .HasOne(ib => ib.PostItem)
                .WithMany(pi => pi.ItemBookings)
                .HasForeignKey(ib => ib.PostItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemBooking ↔ Vehicle (used for transport)
            modelBuilder.Entity<ItemBooking>()
                .HasOne(ib => ib.Vehicle)
                .WithMany() // Không có collection ngược trong Vehicle cho vai trò này
                .HasForeignKey(ib => ib.VehicleId)
                .IsRequired(false) // Vehicle có thể null nếu là external
                .OnDelete(DeleteBehavior.Restrict);


            // VehicleContract ↔ VehicleBooking (1-1)
            modelBuilder.Entity<VehicleContract>()
                .HasOne(vc => vc.VehicleBooking)
                .WithOne(vb => vb.VehicleContract)
                .HasForeignKey<VehicleContract>(vc => vc.VehicleBookingId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemContract ↔ ItemBooking (1-1)
            modelBuilder.Entity<ItemContract>()
                .HasOne(ic => ic.ItemBooking)
                .WithOne(ib => ib.ItemContract)
                .HasForeignKey<ItemContract>(ic => ic.ItemBookingId)
                .OnDelete(DeleteBehavior.Restrict);


            // ContractTemplate ↔ ContractTerm
            modelBuilder.Entity<ContractTerm>()
                .HasOne(ct => ct.ContractTemplate)
                .WithMany(t => t.ContractTerms)
                .HasForeignKey(ct => ct.ContractTemplateId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa template thì term cũng xóa

          


            // ClauseTemplate ↔ ClauseContent
            modelBuilder.Entity<ClauseContent>()
                .HasOne(cc => cc.ClauseTemplate)
                .WithMany() // Không có collection ngược trong ClauseTemplate cho từng content
                .HasForeignKey(cc => cc.ClauseTemplateId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa template thì content cũng xóa


            // VehicleBookingReport ↔ VehicleBooking (1-1)
            modelBuilder.Entity<VehicleBookingReport>()
                .HasOne(vbr => vbr.VehicleBooking)
                .WithMany(vb => vb.Reports) // Changed to WithMany as one booking can have multiple reports
                .HasForeignKey(vbr => vbr.VehicleBookingId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemBookingReport ↔ ItemBooking (1-1)
            modelBuilder.Entity<ItemBookingReport>()
                .HasOne(ibr => ibr.ItemBooking)
                .WithMany(ib => ib.Reports) // Changed to WithMany as one booking can have multiple reports
                .HasForeignKey(ibr => ibr.ItemBookingId)
                .OnDelete(DeleteBehavior.Restrict);


            // BaseReport ↔ ReportTemplate
            modelBuilder.Entity<BaseReport>()
                .HasOne(br => br.ReportTemplate)
                .WithMany() // No inverse collection in ReportTemplate
                .HasForeignKey(br => br.ReportTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            // ReportTerm ↔ ReportTemplate
            modelBuilder.Entity<ReportTerm>()
                .HasOne(rt => rt.ReportTemplate)
                .WithMany(t => t.ReportTerms)
                .HasForeignKey(rt => rt.ReportTemplateId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa template thì term cũng xóa

            


            // Review ↔ Vehicle (optional)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.RelatedVehicle)
                .WithMany(v => v.Reviews)
                .HasForeignKey(r => r.RelatedVehicleId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Review ↔ VehicleBooking (optional)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.RelatedVehicleBooking)
                .WithMany()
                .HasForeignKey(r => r.RelatedVehicleBookingId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Review ↔ ItemBooking (optional)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.RelatedItemBooking)
                .WithMany()
                .HasForeignKey(r => r.RelatedItemBookingId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);


            // Trip ↔ ItemBooking (optional)
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.RelatedItemBooking)
                .WithMany()
                .HasForeignKey(t => t.RelatedItemBookingId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Trip ↔ TripDriver
            modelBuilder.Entity<TripDriver>()
                .HasOne(td => td.Trip)
                .WithMany(t => t.TripDrivers)
                .HasForeignKey(td => td.TripId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa chuyến đi thì tài xế trong chuyến đi cũng xóa

            // TripStepInPlan ↔ Trip
            modelBuilder.Entity<TripStepInPlan>()
                .HasOne(tsp => tsp.Trip)
                .WithMany(t => t.TripSteps)
                .HasForeignKey(tsp => tsp.TripId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa chuyến đi thì các bước cũng xóa

            // TripStepInPlan ↔ TripDriver
            modelBuilder.Entity<TripStepInPlan>()
                .HasOne(tsp => tsp.TripDriver)
                .WithMany() // Không có collection ngược cụ thể trong TripDriver
                .HasForeignKey(tsp => tsp.TripDriverId)
                .OnDelete(DeleteBehavior.Restrict);


            // VehicleInspection ↔ BaseReport
            modelBuilder.Entity<VehicleInspection>()
                .HasOne(vi => vi.Report)
                .WithMany(br => br.VehicleInspections)
                .HasForeignKey(vi => vi.ReportId)
                .OnDelete(DeleteBehavior.Restrict);

            // VehicleInspection ↔ Vehicle
            modelBuilder.Entity<VehicleInspection>()
                .HasOne(vi => vi.Vehicle)
                .WithMany(v => v.Inspections)
                .HasForeignKey(vi => vi.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            //// InspectionResolution ↔ VehicleInspection
            //modelBuilder.Entity<InspectionResolution>()
            //    .HasOne(ir => ir.VehicleInspection)
            //    .WithMany(vi => vi.Resolutions)
            //    .HasForeignKey(ir => ir.VehicleInspectionId)
            //    .OnDelete(DeleteBehavior.Cascade); // Xóa kiểm tra thì giải quyết cũng xóa

            //// InspectionResolution ↔ User (ResolvedBy)
            //modelBuilder.Entity<InspectionResolution>()
            //    .HasOne(ir => ir.ResolvedByUser)
            //    .WithMany()
            //    .HasForeignKey(ir => ir.ResolvedByUserId)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Restrict);


            // UserViolation ↔ UserActivityLog
            modelBuilder.Entity<UserActivityLog>()
                .HasOne(ual => ual.UserViolation)
                .WithMany(uv => uv.RelatedActivityLogs)
                .HasForeignKey(ual => ual.UserViolationId)
                .IsRequired(false) // Log không nhất thiết phải liên quan đến violation
                .OnDelete(DeleteBehavior.Restrict);

            // UserViolation ↔ VehicleBooking (optional)
            modelBuilder.Entity<UserViolation>()
                .HasOne(uv => uv.VehicleBooking)
                .WithMany()
                .HasForeignKey(uv => uv.VehicleBookingId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // UserViolation ↔ ItemBooking (optional)
            modelBuilder.Entity<UserViolation>()
                .HasOne(uv => uv.ItemBooking)
                .WithMany()
                .HasForeignKey(uv => uv.ItemBookingId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);


            // ---------------------- Value Objects Mapping ----------------------

            modelBuilder.Entity<PostItem>(entity =>
            {
                // Mapping cho Location Value Object
                entity.OwnsOne(pi => pi.StartLocation, location =>
                {
                    location.Property(p => p.Address).HasColumnName("StartLocationAddress").IsRequired();
                    location.Property(p => p.Latitude).HasColumnName("StartLocationLatitude").IsRequired();
                    location.Property(p => p.Longitude).HasColumnName("StartLocationLongitude").IsRequired();
                });
                entity.OwnsOne(pi => pi.EndLocation, location =>
                {
                    location.Property(p => p.Address).HasColumnName("EndLocationAddress").IsRequired();
                    location.Property(p => p.Latitude).HasColumnName("EndLocationLatitude").IsRequired();
                    location.Property(p => p.Longitude).HasColumnName("EndLocationLongitude").IsRequired();
                });

                // Mapping cho TimeWindow Value Object
                entity.OwnsOne(pi => pi.PickupTimeWindow, timeWindow =>
                {
                    timeWindow.Property(p => p.StartTime).HasColumnName("PickupTimeWindowStart");
                    timeWindow.Property(p => p.EndTime).HasColumnName("PickupTimeWindowEnd");
                });
                entity.OwnsOne(pi => pi.DeliveryTimeWindow, timeWindow =>
                {
                    timeWindow.Property(p => p.StartTime).HasColumnName("DeliveryTimeWindowStart");
                    timeWindow.Property(p => p.EndTime).HasColumnName("DeliveryTimeWindowEnd");
                });

                // Precision cho các thuộc tính Decimal
                entity.Property(e => e.PricePerUnit).HasPrecision(18, 2);
                entity.Property(e => e.VolumeM3).HasPrecision(10, 3); // Đổi tên từ Volume
                entity.Property(e => e.WeightKg).HasPrecision(10, 3); // Đổi tên từ Weight
            });

            // Mapping cho Location và Time trong Trip
            modelBuilder.Entity<Trip>(entity =>
            {
                entity.OwnsOne(t => t.StartLocation, location =>
                {
                    location.Property(p => p.Address).HasColumnName("StartLocationAddress").IsRequired();
                    location.Property(p => p.Latitude).HasColumnName("StartLocationLatitude").IsRequired();
                    location.Property(p => p.Longitude).HasColumnName("StartLocationLongitude").IsRequired();
                });
                entity.OwnsOne(t => t.EndLocation, location =>
                {
                    location.Property(p => p.Address).HasColumnName("EndLocationAddress").IsRequired();
                    location.Property(p => p.Latitude).HasColumnName("EndLocationLatitude").IsRequired();
                    location.Property(p => p.Longitude).HasColumnName("EndLocationLongitude").IsRequired();
                });
            });

            // Mapping cho Location trong TripStepInPlan
            modelBuilder.Entity<TripStepInPlan>(entity =>
            {
                entity.OwnsOne(tsp => tsp.StartLocation, location =>
                {
                    location.Property(p => p.Address).HasColumnName("StartLocationAddress").IsRequired();
                    location.Property(p => p.Latitude).HasColumnName("StartLocationLatitude").IsRequired();
                    location.Property(p => p.Longitude).HasColumnName("StartLocationLongitude").IsRequired();
                });
                entity.OwnsOne(tsp => tsp.EndLocation, location =>
                {
                    location.Property(p => p.Address).HasColumnName("EndLocationAddress").IsRequired();
                    location.Property(p => p.Latitude).HasColumnName("EndLocationLatitude").IsRequired();
                    location.Property(p => p.Longitude).HasColumnName("EndLocationLongitude").IsRequired();
                });
            });


            // Precision setup
            modelBuilder.Entity<VehicleBooking>().Property(v => v.TotalPrice).HasPrecision(18, 2);
            modelBuilder.Entity<ItemBooking>().Property(i => i.TotalPrice).HasPrecision(18, 2);
            modelBuilder.Entity<PostVehicle>().Property(p => p.DailyPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Rule>().Property(r => r.Value).HasPrecision(18, 2);
            modelBuilder.Entity<Transaction>().Property(t => t.Amount).HasPrecision(18, 2);
            modelBuilder.Entity<Wallet>().Property(w => w.CurrentBalance).HasPrecision(18, 2);
            

            modelBuilder.Entity<PostItem>(entity =>
            {
                entity.Property(e => e.PricePerUnit).HasPrecision(18, 2);
                entity.Property(e => e.Quantity).HasPrecision(10, 3);
                entity.Property(e => e.WeightKg).HasPrecision(10, 3);
            });


            // ---------------------- Seed Data ----------------------
            DbSeeder.Seed(modelBuilder);
        }
    }
}

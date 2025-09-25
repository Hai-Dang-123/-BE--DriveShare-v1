using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class aa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractTemplate",
                columns: table => new
                {
                    ContractTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTemplate", x => x.ContractTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    RuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RuleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RuleCategory = table.Column<int>(type: "int", nullable: false),
                    RuleValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RuleUnit = table.Column<int>(type: "int", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RuleStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.RuleId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    VehicleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.VehicleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: false),
                    ResponseComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlannedDistanceKm = table.Column<double>(type: "float", nullable: false),
                    ActualDistanceKm = table.Column<double>(type: "float", nullable: true),
                    ETA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_Trips_Users_CreateById",
                        column: x => x.CreateById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenType = table.Column<int>(type: "int", nullable: false),
                    TokenValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.UserTokenId);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VerificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "VehicleTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalletId);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripDrivers",
                columns: table => new
                {
                    TripDriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExternalDriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalDriverLicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalDriverPhone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripDrivers", x => x.TripDriverId);
                    table.ForeignKey(
                        name: "FK_TripDrivers_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripDrivers_Users_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostVehicles",
                columns: table => new
                {
                    PostVehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DailyPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostVehicles", x => x.PostVehicleId);
                    table.ForeignKey(
                        name: "FK_PostVehicles_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostVehicles_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleImages",
                columns: table => new
                {
                    VehicleImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleImages", x => x.VehicleImageId);
                    table.ForeignKey(
                        name: "FK_VehicleImages_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Verifications",
                columns: table => new
                {
                    VerificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocType = table.Column<int>(type: "int", nullable: false),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verifications", x => x.VerificationId);
                    table.ForeignKey(
                        name: "FK_Verifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Verifications_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripStepInPlans",
                columns: table => new
                {
                    TripStepInPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripDriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StepNumber = table.Column<int>(type: "int", nullable: false),
                    StartLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartLatitude = table.Column<double>(type: "float", nullable: false),
                    StartLongitude = table.Column<double>(type: "float", nullable: false),
                    EndLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndLatitude = table.Column<double>(type: "float", nullable: false),
                    EndLongitude = table.Column<double>(type: "float", nullable: false),
                    DistanceKm = table.Column<double>(type: "float", nullable: false),
                    ETA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoadType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripStepInPlans", x => x.TripStepInPlanId);
                    table.ForeignKey(
                        name: "FK_TripStepInPlans_TripDrivers_TripDriverId",
                        column: x => x.TripDriverId,
                        principalTable: "TripDrivers",
                        principalColumn: "TripDriverId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripStepInPlans_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostVehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_PostVehicles_PostVehicleId",
                        column: x => x.PostVehicleId,
                        principalTable: "PostVehicles",
                        principalColumn: "PostVehicleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerSigned = table.Column<bool>(type: "bit", nullable: false),
                    RenterSigned = table.Column<bool>(type: "bit", nullable: false),
                    ContractTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_Contracts_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractTemplate_ContractTemplateId",
                        column: x => x.ContractTemplateId,
                        principalTable: "ContractTemplate",
                        principalColumn: "ContractTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractTerms",
                columns: table => new
                {
                    ContractTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TermType = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTerms", x => x.ContractTermId);
                    table.CheckConstraint("CK_ContractTerm_Parent", "(ContractTemplateId IS NOT NULL AND ContractId IS NULL) \r\n              OR (ContractTemplateId IS NULL AND ContractId IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_ContractTerms_ContractTemplate_ContractTemplateId",
                        column: x => x.ContractTemplateId,
                        principalTable: "ContractTemplate",
                        principalColumn: "ContractTemplateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractTerms_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ContractTemplate",
                columns: new[] { "ContractTemplateId", "Version" },
                values: new object[] { new Guid("62345678-90ab-cdef-1234-567890abcdef"), "v1.0" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { new Guid("a1dab1c3-6d48-4b23-8369-2d1c9c828f22"), "Driver" },
                    { new Guid("b2dab1c3-6d48-4b23-8369-2d1c9c828f22"), "Owner" },
                    { new Guid("d4dab1c3-6d48-4b23-8369-2d1c9c828f22"), "Admin" },
                    { new Guid("e3dab1c3-6d48-4b23-8369-2d1c9c828f22"), "Staff" }
                });

            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "VehicleTypeId", "VehicleTypeName" },
                values: new object[] { new Guid("52345678-90ab-cdef-1234-567890abcdef"), "Container" });

            migrationBuilder.InsertData(
                table: "ContractTerms",
                columns: new[] { "ContractTermId", "Content", "ContractId", "ContractTemplateId", "IsMandatory", "TermType" },
                values: new object[,]
                {
                    { new Guid("72345678-90ab-cdef-1234-567890abcdef"), "Bên thuê phải trả xe đúng giờ, đúng tình trạng ban đầu.", null, new Guid("62345678-90ab-cdef-1234-567890abcdef"), true, 0 },
                    { new Guid("82345678-90ab-cdef-1234-567890abcdef"), "Chủ xe có quyền kiểm tra xe sau khi hợp đồng kết thúc.", null, new Guid("62345678-90ab-cdef-1234-567890abcdef"), true, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "LastLoginAt", "PasswordHash", "PhoneNumber", "RoleId", "UserName", "UserStatus" },
                values: new object[,]
                {
                    { new Guid("12345678-90ab-cdef-1234-567890abcdef"), "admin@gmail.com", null, "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm", "0123456789", new Guid("d4dab1c3-6d48-4b23-8369-2d1c9c828f22"), "Admin_Name", 0 },
                    { new Guid("22345678-90ab-cdef-1234-567890abcdef"), "driver@gmail.com", null, "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm", "0123456789", new Guid("a1dab1c3-6d48-4b23-8369-2d1c9c828f22"), "Driver_Name", 0 },
                    { new Guid("32345678-90ab-cdef-1234-567890abcdef"), "owner@gmail.com", null, "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm", "0123456789", new Guid("b2dab1c3-6d48-4b23-8369-2d1c9c828f22"), "Owner_Name", 0 },
                    { new Guid("42345678-90ab-cdef-1234-567890abcdef"), "staff@gmail.com", null, "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm", "0123456789", new Guid("e3dab1c3-6d48-4b23-8369-2d1c9c828f22"), "Staff_Name", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PostVehicleId",
                table: "Bookings",
                column: "PostVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_BookingId",
                table: "Contracts",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractTemplateId",
                table: "Contracts",
                column: "ContractTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTerms_ContractId",
                table: "ContractTerms",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTerms_ContractTemplateId",
                table: "ContractTerms",
                column: "ContractTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostVehicles_OwnerId",
                table: "PostVehicles",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PostVehicles_VehicleId",
                table: "PostVehicles",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_FromUserId",
                table: "Reviews",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ToUserId",
                table: "Reviews",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_TripDrivers_DriverId",
                table: "TripDrivers",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_TripDrivers_TripId",
                table: "TripDrivers",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CreateById",
                table: "Trips",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_TripStepInPlans_TripDriverId",
                table: "TripStepInPlans",
                column: "TripDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_TripStepInPlans_TripId",
                table: "TripStepInPlans",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleImages_VehicleId",
                table: "VehicleImages",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypeId",
                table: "Vehicles",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Verifications_UserId",
                table: "Verifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Verifications_VehicleId",
                table: "Verifications",
                column: "VehicleId",
                unique: true,
                filter: "[VehicleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractTerms");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TripStepInPlans");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "VehicleImages");

            migrationBuilder.DropTable(
                name: "Verifications");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "TripDrivers");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "ContractTemplate");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "PostVehicles");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VehicleTypes");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

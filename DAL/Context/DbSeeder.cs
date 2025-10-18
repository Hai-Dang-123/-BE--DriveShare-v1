using Common.Enums;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class DbSeeder
    {
        private readonly DriverShareAppContext _context;

        // Role-IDs
        private static readonly Guid AdminRole = Guid.Parse("D4DAB1C3-6D48-4B23-8369-2D1C9C828F22");
        private static readonly Guid DriverRole = Guid.Parse("A1DAB1C3-6D48-4B23-8369-2D1C9C828F22");
        private static readonly Guid OwnerRole = Guid.Parse("B2DAB1C3-6D48-4B23-8369-2D1C9C828F22");
        private static readonly Guid StaffRole = Guid.Parse("E3DAB1C3-6D48-4B23-8369-2D1C9C828F22");

        // User-IDs
        private static readonly Guid AdminID = Guid.Parse("12345678-90AB-CDEF-1234-567890ABCDEF");
        private static readonly Guid DriverID = Guid.Parse("22345678-90AB-CDEF-1234-567890ABCDEF");
        private static readonly Guid OwnerID = Guid.Parse("32345678-90AB-CDEF-1234-567890ABCDEF");
        private static readonly Guid StaffID = Guid.Parse("42345678-90AB-CDEF-1234-567890ABCDEF");

        // VehicleType-IDs
        private static readonly Guid Container_Type_Id = Guid.Parse("52345678-90AB-CDEF-1234-567890ABCDEF");

        // ContractTemplate-IDs
        private static readonly Guid BaseContractTemplateId = Guid.Parse("62345678-90AB-CDEF-1234-567890ABCDEF");

        // ContractTerm-IDs
        private static readonly Guid Term1Id = Guid.Parse("72345678-90AB-CDEF-1234-567890ABCDEF");
        private static readonly Guid Term2Id = Guid.Parse("82345678-90AB-CDEF-1234-567890ABCDEF");
        public DbSeeder(DriverShareAppContext context)
        {
            _context = context;
        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedRole(modelBuilder);
            SeedUser(modelBuilder);
            SeedVehicleType(modelBuilder);
            //SeedContractTemplate(modelBuilder);
            //SeedContractTerms(modelBuilder);
        }

        private static void SeedRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = AdminRole,
                    RoleName = "Admin",
                },
                new Role
                {
                    RoleId = DriverRole,
                    RoleName = "Driver",
                },
                new Role
                {
                    RoleId = OwnerRole,
                    RoleName = "Owner",
                },
                new Role
                {
                    RoleId = StaffRole,
                    RoleName = "Staff",
                }

            );
        }

        private static void SeedUser(ModelBuilder modelBuilder)
        {
            //pass : 123
            string fixedHashedPassword = "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm";

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = AdminID,
                    Username = "Admin_Name",
                    Email = "admin@gmail.com",
                    RoleId = AdminRole,
                    PasswordHash = fixedHashedPassword,
                    PhoneNumber = "0123456789",
                    Status = UserStatus.ACTIVE
                },
                new User
                {
                    UserId = DriverID,
                    Username = "Driver_Name",
                    Email = "driver@gmail.com",
                    RoleId = DriverRole,
                    PasswordHash = fixedHashedPassword,
                    PhoneNumber = "0123456789",
                    Status = UserStatus.ACTIVE
                },
                new User
                {
                    UserId = OwnerID,
                    Username = "Owner_Name",
                    Email = "owner@gmail.com",
                    RoleId = OwnerRole,
                    PasswordHash = fixedHashedPassword,
                    PhoneNumber = "0123456789",
                    Status = UserStatus.ACTIVE
                },
                new User
                {
                    UserId = StaffID,
                    Username = "Staff_Name",
                    Email = "staff@gmail.com",
                    RoleId = StaffRole,
                    PasswordHash = fixedHashedPassword,
                    PhoneNumber = "0123456789",
                    Status = UserStatus.ACTIVE
                }

            );
        }

        private static void SeedVehicleType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleType>().HasData(
                new VehicleType
                {

                    VehicleTypeId = Container_Type_Id,
                    Name = "Container",
                    
                }
            );
        }

        //private static void SeedContractTemplate(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ContractTemplate>().HasData(
        //        new ContractTemplate
        //        {
        //            ContractTemplateId = BaseContractTemplateId,
        //            Version = "v1.0"
        //        }
        //    );
        //}
        //private static void SeedContractTerms(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ContractTerm>().HasData(
        //        new ContractTerm
        //        {
        //            ContractTermId = Term1Id,
        //            ContractTemplateId = BaseContractTemplateId,
        //            ContractId = null,
        //            TermType = TermType.CONTRACT,
        //            Content = "Bên thuê phải trả xe đúng giờ, đúng tình trạng ban đầu.",
        //            IsMandatory = true
        //        },
        //        new ContractTerm
        //        {
        //            ContractTermId = Term2Id,
        //            ContractTemplateId = BaseContractTemplateId,
        //            ContractId = null,
        //            TermType = TermType.CONTRACT,
        //            Content = "Chủ xe có quyền kiểm tra xe sau khi hợp đồng kết thúc.",
        //            IsMandatory = true
        //        }
        //    );
        //}
    }
}

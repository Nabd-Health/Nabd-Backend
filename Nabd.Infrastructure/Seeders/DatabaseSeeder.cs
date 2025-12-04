using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.Identity;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Entities.Operations;
using Nabd.Core.Entities.Pharmacy;
using Nabd.Core.Entities.Profiles;
using Nabd.Core.Entities.System;
using Nabd.Core.Enums;
using Nabd.Core.Enums.Identity;
using Nabd.Core.Enums.Medical;
using Nabd.Core.Enums.Operations;
using Nabd.Core.Interfaces;
using Nabd.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Seeders
{
    public class DatabaseSeeder : IDbSeeder
    {
        private readonly NabdDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DatabaseSeeder(
            NabdDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            // 1. التأكد من إنشاء قاعدة البيانات
            // (في الإنتاج بنستخدم Migrations، بس هنا للتأكيد)
            await _context.Database.MigrateAsync();

            // 2. Roles & Users (الأهم)
            await SeedRolesAsync();
            await SeedUsersAndProfilesAsync();

            // 3. System Data (Lookups)
            await SeedSystemParametersAsync();
            await SeedMedicationsAsync();

            // 4. Operations Data (Dependent on Doctors)
            if (await _context.Doctors.AnyAsync())
            {
                await SeedClinicBranchesAsync();
            }

            // حفظ التغييرات النهائية
            await _context.SaveChangesAsync();
        }

        // =============================================================
        // 1. Roles
        // =============================================================
        private async Task SeedRolesAsync()
        {
            if (await _roleManager.Roles.AnyAsync()) return;

            var roles = new List<Role>
            {
                new Role { Name = "Admin", Description = "System Administrator" },
                new Role { Name = "Doctor", Description = "Medical Service Provider" },
                new Role { Name = "Patient", Description = "Service Receiver" }
            };

            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }
        }

        // =============================================================
        // 2. Users & Profiles (Doctor & Patient)
        // =============================================================
        private async Task SeedUsersAndProfilesAsync()
        {
            if (await _userManager.Users.AnyAsync()) return;

            // --- 1. Admin ---
            var adminUser = new AppUser
            {
                FirstName = "Nabd",
                LastName = "Admin",
                Email = "admin@nabd.com",
                UserName = "admin@nabd.com",
                UserType = UserType.Admin,
                EmailConfirmed = true,
                PhoneNumber = "01000000000"
            };
            await _userManager.CreateAsync(adminUser, "P@ssword123");
            await _userManager.AddToRoleAsync(adminUser, "Admin");

            // --- 2. Doctor ---
            var doctorUser = new AppUser
            {
                FirstName = "Seif",
                LastName = "Eldin",
                Email = "dr.seif@nabd.com",
                UserName = "dr.seif@nabd.com",
                UserType = UserType.Doctor,
                EmailConfirmed = true,
                PhoneNumber = "01234567890"
            };
            var result = await _userManager.CreateAsync(doctorUser, "P@ssword123");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(doctorUser, "Doctor");

                // إنشاء البروفايل فوراً
                var doctorProfile = new Doctor
                {
                    AppUserId = doctorUser.Id,
                    AppUser = doctorUser,
                    FullName = "Dr. Seif Eldin",
                    Specialization = MedicalSpecialty.Cardiology,
                    Bio = "Expert Cardiologist with 10 years of experience.",
                    ConsultationFee = 500,
                    Status = DoctorStatus.Active, // مفعل جاهز
                    VerifiedAt = DateTime.UtcNow,
                    IsAvailable = true
                };
                await _context.Doctors.AddAsync(doctorProfile);
            }

            // --- 3. Patient ---
            var patientUser = new AppUser
            {
                FirstName = "Ahmed",
                LastName = "Mohamed",
                Email = "patient@nabd.com",
                UserName = "patient@nabd.com",
                UserType = UserType.Patient,
                EmailConfirmed = true,
                PhoneNumber = "01111111111"
            };
            var pResult = await _userManager.CreateAsync(patientUser, "P@ssword123");

            if (pResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(patientUser, "Patient");

                var patientProfile = new Patient
                {
                    AppUserId = patientUser.Id,
                    AppUser = patientUser,
                    FullName = "Ahmed Mohamed",
                    NationalId = "29901011234567",
                    DateOfBirth = new DateTime(1999, 1, 1),
                    Gender = Gender.Male
                };
                await _context.Patients.AddAsync(patientProfile);
            }
        }

        // =============================================================
        // 3. System Parameters
        // =============================================================
        private async Task SeedSystemParametersAsync()
        {
            if (await _context.SystemParameters.AnyAsync()) return;

            var paramsList = new List<SystemParameter>
            {
                new SystemParameter { Key = "TaxPercentage", Value = "14", Description = "VAT Percentage", Group = "Finance" },
                new SystemParameter { Key = "PlatformFee", Value = "10", Description = "Nabd Commission %", Group = "Finance" },
                new SystemParameter { Key = "EnableAI", Value = "true", Description = "Master switch for AI features", Group = "AI" }
            };

            await _context.SystemParameters.AddRangeAsync(paramsList);
        }

        // =============================================================
        // 4. Medications (Pharmacy)
        // =============================================================
        private async Task SeedMedicationsAsync()
        {
            if (await _context.Medications.AnyAsync()) return;

            var meds = new List<Medication>
            {
                new Medication {
                    TradeName = "Panadol Advance",
                    ScientificName = "Paracetamol",
                    Strength = "500mg",
                    Form = MedicationForm.Tablet,
                    Manufacturer = "GSK",
                    Description = "For mild to moderate pain."
                },
                new Medication {
                    TradeName = "Augmentin",
                    ScientificName = "Amoxicillin / Clavulanic acid",
                    Strength = "1g",
                    Form = MedicationForm.Tablet,
                    Manufacturer = "GSK",
                    Description = "Broad spectrum antibiotic."
                },
                new Medication {
                    TradeName = "Cataflam",
                    ScientificName = "Diclofenac Potassium",
                    Strength = "50mg",
                    Form = MedicationForm.Tablet,
                    Manufacturer = "Novartis",
                    Description = "Anti-inflammatory and analgesic."
                }
            };

            await _context.Medications.AddRangeAsync(meds);
        }

        // =============================================================
        // 5. Clinic Branches (Operations)
        // =============================================================
        private async Task SeedClinicBranchesAsync()
        {
            if (await _context.ClinicBranches.AnyAsync()) return;

            // هنجيب الدكتور اللي لسه عاملينه
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.AppUser.Email == "dr.seif@nabd.com");
            if (doctor == null) return;

            var branch = new ClinicBranch
            {
                DoctorId = doctor.Id,
                Doctor = doctor,
                Name = "عيادة الزقازيق - القومية",
                PhoneNumber = "0552345678",
                City = "Zagazig",
                Governorate = Governorate.Sharqia,
                StreetAddress = "شارع طلبة عويضة، أمام المصرية بلازا",
                Latitude = 30.5876,
                Longitude = 31.5035,
                IsActive = true
            };

            await _context.ClinicBranches.AddAsync(branch);
        }
    }
}
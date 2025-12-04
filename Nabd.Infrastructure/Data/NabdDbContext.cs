using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.AI;
using Nabd.Core.Entities.Feedback;
using Nabd.Core.Entities.Identity;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Entities.Operations;
using Nabd.Core.Entities.Pharmacy;
using Nabd.Core.Entities.Profiles;
using Nabd.Core.Entities.System;
using System;
using System.Linq;
using System.Reflection;

namespace Nabd.Infrastructure.Data
{
    // ✅ الوراثة من IdentityDbContext ضرورية
    public class NabdDbContext : IdentityDbContext<AppUser, Role, Guid>
    {
        public NabdDbContext(DbContextOptions<NabdDbContext> options) : base(options)
        {
        }

        // =========================================================
        // 1. الموديول الأساسي (Profiles)
        // =========================================================
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DoctorDocument> DoctorDocuments { get; set; }

        // =========================================================
        // 2. الموديول الطبي (Clinical)
        // =========================================================
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ConsultationRecord> ConsultationRecords { get; set; }
        public DbSet<MedicalHistoryItem> MedicalHistoryItems { get; set; }
        public DbSet<MedicalAttachment> MedicalAttachments { get; set; }

        // =========================================================
        // 3. موديول العمليات (Operations)
        // =========================================================
        public DbSet<ClinicBranch> ClinicBranches { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

        // =========================================================
        // 4. موديول الصيدلية (Pharmacy)
        // =========================================================
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }

        // =========================================================
        // 5. موديول الذكاء والتقييم (AI & Feedback)
        // =========================================================
        public DbSet<DoctorReview> DoctorReviews { get; set; }
        public DbSet<AIDiagnosisLog> AIDiagnosisLogs { get; set; }

        // =========================================================
        // 6. موديول النظام والأمان (System & Security)
        // =========================================================
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<SystemParameter> SystemParameters { get; set; }

        // =========================================================
        // ضبط العلاقات (Fluent API)
        // =========================================================
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // ⚠️ إجباري لـ Identity

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // -----------------------------------------------------
            // 1. علاقات الدكتور (Doctor)
            // -----------------------------------------------------
            // حذف الدكتور -> يحذف الفروع (Cascade)
            builder.Entity<Doctor>()
                .HasMany(d => d.ClinicBranches)
                .WithOne(b => b.Doctor)
                .HasForeignKey(b => b.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            // حذف الدكتور -> لا يحذف الروشتات (Restrict) حفاظاً على التاريخ الطبي
            builder.Entity<Doctor>()
                .HasMany(d => d.Prescriptions)
                .WithOne(p => p.Doctor)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------------------------------
            // 2. علاقات المريض (Patient)
            // -----------------------------------------------------
            builder.Entity<Patient>()
                .HasIndex(p => p.NationalId).IsUnique();

            // حذف المريض -> ممنوع لو عنده مواعيد (Restrict)
            builder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // حذف المريض -> يحذف تاريخه المرضي (Cascade) لأنه خاص به فقط
            builder.Entity<Patient>()
                .HasMany(p => p.MedicalHistoryItems)
                .WithOne(m => m.Patient)
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // -----------------------------------------------------
            // 3. علاقات الجداول والعيادات (Doctor Schedules) - حل مشكلة الـ Cycle
            // -----------------------------------------------------

            // الفرع هو المالك للجدول: حذف الفرع -> يحذف الجدول (Cascade)
            builder.Entity<ClinicBranch>()
                .HasMany(b => b.Schedules)
                .WithOne(s => s.ClinicBranch)
                .HasForeignKey(s => s.ClinicBranchId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ التعديل الحاسم: جعل الحذف NoAction لمنع الدورة المغلقة
            builder.Entity<DoctorSchedule>()
                .HasOne(s => s.Doctor)
                .WithMany()
                .HasForeignKey(s => s.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            // -----------------------------------------------------
            // 4. علاقات التقييمات (Doctor Reviews) - حل مشكلة الـ Cycle الثانية
            // -----------------------------------------------------

            // الموعد هو المالك للتقييم (Cascade)
            builder.Entity<DoctorReview>()
                .HasOne(r => r.Appointment)
                .WithOne(a => a.DoctorReview)
                .HasForeignKey<DoctorReview>(r => r.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // كسر الـ Cycle مع الدكتور (NoAction)
            builder.Entity<DoctorReview>()
                .HasOne(r => r.Doctor)
                .WithMany(d => d.DoctorReviews)
                .HasForeignKey(r => r.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            // كسر الـ Cycle مع المريض (NoAction)
            builder.Entity<DoctorReview>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.DoctorReviews)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            // -----------------------------------------------------
            // 5. علاقات الروشتة (Prescription)
            // -----------------------------------------------------
            builder.Entity<Prescription>()
                .HasMany(p => p.PrescriptionItems)
                .WithOne(i => i.Prescription)
                .HasForeignKey(i => i.PrescriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Appointment>()
                .HasOne(a => a.ConsultationRecord)
                .WithOne(c => c.Appointment)
                .HasForeignKey<ConsultationRecord>(c => c.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------------------------------
            // 6. ضبط الأسعار (Decimal Precision)
            // -----------------------------------------------------
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
        }
    }
}
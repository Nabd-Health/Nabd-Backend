using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities;
using System.Reflection;

namespace Nabd.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // =========================================================
        // 1. Clinical Core (الموديول الطبي الأساسي)
        // =========================================================
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<ConsultationRecord> ConsultationRecords { get; set; }

        // =========================================================
        // 2. Operations & Scheduling (العمليات والمواعيد)
        // =========================================================
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ClinicBranch> ClinicBranches { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

        // =========================================================
        // 3. E-Prescription (الصيدلية والروشتة)
        // =========================================================
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }

        // =========================================================
        // 4. AI & MLOps (الذكاء الاصطناعي)
        // =========================================================
        public DbSet<AIDiagnosisLog> AIDiagnosisLogs { get; set; }

        // =========================================================
        // 5. Archiving (المرفقات)
        // =========================================================
        public DbSet<MedicalAttachment> MedicalAttachments { get; set; }

        // =========================================================
        // 6. Security & Auditing (الأمان)
        // =========================================================
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }


        // =========================================================
        // ضبط العلاقات والقيود (Fluent API Configuration)
        // =========================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تطبيق أي إعدادات خارجية (Best Practice)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // -----------------------------------------------------
            // علاقات الدكتور (Doctor Relationships)
            // -----------------------------------------------------

            // الدكتور والفروع (One-to-Many)
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.ClinicBranches) // لازم تكون ضفت دي في Doctor.cs (ICollection<ClinicBranch>)
                .WithOne(b => b.Doctor)
                .HasForeignKey(b => b.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // الدكتور والروشتات (One-to-Many)
            modelBuilder.Entity<Doctor>()
                .HasMany<Prescription>()
                .WithOne(p => p.Doctor)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------------------------------
            // علاقات المريض (Patient Relationships)
            // -----------------------------------------------------

            // المريض والمواعيد (One-to-Many)
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // المريض والروشتات (One-to-Many)
            modelBuilder.Entity<Patient>()
                .HasMany<Prescription>()
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------------------------------
            // علاقات المواعيد والكشف (Appointment & Consultation)
            // -----------------------------------------------------

            // الموعد والكشف (One-to-One)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.ConsultationRecord)
                .WithOne(c => c.Appointment)
                .HasForeignKey<ConsultationRecord>(c => c.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------------------------------
            // علاقات الروشتة (Prescription)
            // -----------------------------------------------------

            // الكشف والروشتة (One-to-Many) - لأن الكشف ممكن يطلع كذا روشتة
            modelBuilder.Entity<ConsultationRecord>()
                .HasMany<Prescription>() // لازم تضيف ICollection<Prescription> في ConsultationRecord
                .WithOne(p => p.ConsultationRecord)
                .HasForeignKey(p => p.ConsultationRecordId)
                .OnDelete(DeleteBehavior.Restrict);

            // الروشتة والأدوية (Composition: One-to-Many)
            // لو مسحنا الروشتة، بنمسح سطور الأدوية اللي جواها (Cascade هنا مسموح لأنه جزء منها)
            modelBuilder.Entity<Prescription>()
                .HasMany(p => p.Items)
                .WithOne(i => i.Prescription)
                .HasForeignKey(i => i.PrescriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            // -----------------------------------------------------
            // علاقات الفروع والمواعيد (Branch & Schedule)
            // -----------------------------------------------------

            // الفرع وجداول المواعيد (Composition)
            modelBuilder.Entity<ClinicBranch>()
                .HasMany(b => b.Schedules)
                .WithOne(s => s.ClinicBranch)
                .HasForeignKey(s => s.ClinicBranchId)
                .OnDelete(DeleteBehavior.Cascade);

            // -----------------------------------------------------
            // تحسينات الأداء (Indexing) - للمشروع الكبير
            // -----------------------------------------------------

            // فهرس على الرقم القومي للمريض (عشان البحث يكون طيارة)
            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.NationalId)
                .IsUnique();

            // فهرس على كود الروشتة
            modelBuilder.Entity<Prescription>()
                .HasIndex(p => p.UniqueCode)
                .IsUnique();
        }
    }
}
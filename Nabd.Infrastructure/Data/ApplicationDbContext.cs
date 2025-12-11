
  using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.AI;
using Nabd.Core.Entities.Identity;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Entities.Operations;
using Nabd.Core.Entities.Pharmacy;
using Nabd.Core.Entities.Profiles;
using Nabd.Core.Entities.System;
using System.Reflection;

namespace Nabd.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // =========================================================
        // 1. Clinical Core 
        // =========================================================
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<ConsultationRecord> ConsultationRecords { get; set; }

        // =========================================================
        // 2. Operations & Scheduling 
        // =========================================================
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ClinicBranch> ClinicBranches { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

        // =========================================================
        // 3. E-Prescription 
        // =========================================================
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }

        // =========================================================
        // 4. AI & MLOps 
        // =========================================================
        public DbSet<AIDiagnosisLog> AIDiagnosisLogs { get; set; }

        // =========================================================
        // 5. Archiving 
        // =========================================================
        public DbSet<MedicalAttachment> MedicalAttachments { get; set; }

        // =========================================================
        // 6. Security & Auditing 
        // =========================================================
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }


        // =========================================================
        //  (Fluent API Configuration)
        // =========================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // -----------------------------------------------------
            // (Doctor Relationships)
            // -----------------------------------------------------

            //(One-to-Many)
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.ClinicBranches) 
                .WithOne(b => b.Doctor)
                .HasForeignKey(b => b.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            //  (One-to-Many)
            modelBuilder.Entity<Doctor>()
                .HasMany<Prescription>()
                .WithOne(p => p.Doctor)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------------------------------
            // (Patient Relationships)
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
            // (Appointment & Consultation)
            // -----------------------------------------------------

            //  (One-to-One)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.ConsultationRecord)
                .WithOne(c => c.Appointment)
                .HasForeignKey<ConsultationRecord>(c => c.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------------------------------
            //  (Prescription)
            // -----------------------------------------------------

            // الكشف والروشتة (One-to-Many) 
            modelBuilder.Entity<ConsultationRecord>()
                .HasMany<Prescription>() 
                .WithOne(p => p.ConsultationRecord)
                .HasForeignKey(p => p.ConsultationRecordId)
                .OnDelete(DeleteBehavior.Restrict);

            // الروشتة والأدوية (Composition: One-to-Many)
           
    modelBuilder.Entity<Prescription>()
    .HasMany(p => p.PrescriptionItems) 
    .WithOne(i => i.Prescription)
    .HasForeignKey(i => i.PrescriptionId)
    .OnDelete(DeleteBehavior.Cascade);
            // -----------------------------------------------------
            // (Branch & Schedule)
            // -----------------------------------------------------

          
            modelBuilder.Entity<ClinicBranch>()
                .HasMany(b => b.Schedules)
                .WithOne(s => s.ClinicBranch)
                .HasForeignKey(s => s.ClinicBranchId)
                .OnDelete(DeleteBehavior.Cascade);

            // -----------------------------------------------------
            // (Indexing)
            // -----------------------------------------------------

           
            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.NationalId)
                .IsUnique();

    
            modelBuilder.Entity<Prescription>()
                .HasIndex(p => p.UniqueCode)
                .IsUnique();
        }
    }
}

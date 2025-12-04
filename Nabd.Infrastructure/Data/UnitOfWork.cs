using Microsoft.EntityFrameworkCore.Storage;
using Nabd.Core.Interfaces;
using Nabd.Core.Interfaces.Repositories.AI;
using Nabd.Core.Interfaces.Repositories.Feedback;
using Nabd.Core.Interfaces.Repositories.Identity;
using Nabd.Core.Interfaces.Repositories.Medical;
using Nabd.Core.Interfaces.Repositories.Operations;
using Nabd.Core.Interfaces.Repositories.Pharmacy;
using Nabd.Core.Interfaces.Repositories.Profiles;
using Nabd.Infrastructure.Repositories.AI;
using Nabd.Infrastructure.Repositories.Feedback;
using Nabd.Infrastructure.Repositories.Identity;
using Nabd.Infrastructure.Repositories.Medical;
using Nabd.Infrastructure.Repositories.Operations;
using Nabd.Infrastructure.Repositories.Pharmacy;
using Nabd.Infrastructure.Repositories.Profiles;
using System;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NabdDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(NabdDbContext context)
        {
            _context = context;

            // Identity
            Users = new AppUserRepository(_context); // تم تعديل الاسم لـ Users
            RefreshTokens = new RefreshTokenRepository(_context);

            // Profiles
            Doctors = new DoctorRepository(_context);
            Patients = new PatientRepository(_context);
            DoctorDocuments = new DoctorDocumentRepository(_context);

            // Medical
            Appointments = new AppointmentRepository(_context);
            ConsultationRecords = new ConsultationRecordRepository(_context);
            MedicalHistoryItems = new MedicalHistoryItemRepository(_context);

            // Operations
            DoctorSchedules = new DoctorScheduleRepository(_context);
            ClinicBranches = new ClinicBranchRepository(_context);

            // Feedback
            DoctorReviews = new DoctorReviewRepository(_context);

            // Pharmacy
            Medications = new MedicationRepository(_context);
            Prescriptions = new PrescriptionRepository(_context);

            // AI
            AIDiagnosisLogs = new AIDiagnosisLogRepository(_context);
        }

        // ==========================================
        // Properties Implementation (Must match Interface)
        // ==========================================
        public IAppUserRepository Users { get; private set; } // كان اسمها AppUsers
        public IRefreshTokenRepository RefreshTokens { get; private set; }

        public IDoctorRepository Doctors { get; private set; }
        public IPatientRepository Patients { get; private set; }
        public IDoctorDocumentRepository DoctorDocuments { get; private set; }

        public IAppointmentRepository Appointments { get; private set; }
        public IConsultationRecordRepository ConsultationRecords { get; private set; }
        public IMedicalHistoryItemRepository MedicalHistoryItems { get; private set; }

        public IDoctorScheduleRepository DoctorSchedules { get; private set; }
        public IClinicBranchRepository ClinicBranches { get; private set; }

        public IDoctorReviewRepository DoctorReviews { get; private set; }

        public IMedicationRepository Medications { get; private set; }
        public IPrescriptionRepository Prescriptions { get; private set; }

        // افترضنا أنك ستضيف هذا للإنترفيس لاحقاً
        public IAIDiagnosisLogRepository AIDiagnosisLogs { get; private set; }

        // ==========================================
        // Transaction Management
        // ==========================================

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                }
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
using System;
using System.Threading.Tasks;
using Nabd.Core.Interfaces.Repositories.Base;
using Nabd.Core.Interfaces.Repositories.Identity;
using Nabd.Core.Interfaces.Repositories.Feedback;
using Nabd.Core.Interfaces.Repositories.Profiles;
using Nabd.Core.Interfaces.Repositories.Medical;
using Nabd.Core.Interfaces.Repositories.Operations;
using Nabd.Core.Interfaces.Repositories.Pharmacy;
using Nabd.Core.Interfaces.Repositories.AI;
using Nabd.Core.Interfaces.Repositories.System; 

namespace Nabd.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // ==========================================
        // 1. Identity & Profiles
        // ==========================================
        IAppUserRepository Users { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IDoctorRepository Doctors { get; }
        IPatientRepository Patients { get; }
        IDoctorDocumentRepository DoctorDocuments { get; }

        // ==========================================
        // 2. Medical Core
        // ==========================================
        IAppointmentRepository Appointments { get; }
        IConsultationRecordRepository ConsultationRecords { get; }
        IMedicalHistoryItemRepository MedicalHistoryItems { get; }

        // ==========================================
        // 3. Operations & Clinics
        // ==========================================
        IClinicBranchRepository ClinicBranches { get; }
        IDoctorScheduleRepository DoctorSchedules { get; }

        // ==========================================
        // 4. Pharmacy
        // ==========================================
        IMedicationRepository Medications { get; }
        IPrescriptionRepository Prescriptions { get; }

        // ==========================================
        // 5. Feedback & AI
        // ==========================================
        IDoctorReviewRepository DoctorReviews { get; }
        IAIDiagnosisLogRepository AIDiagnosisLogs { get; }

        // ==========================================
        // 6. System 
        // ==========================================
        IParameterRepository SystemParameters { get; }

        // ==========================================
        // Core Actions
        // ==========================================
        Task<int> CompleteAsync(); 
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
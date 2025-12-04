using System;
using System.Threading.Tasks;
using Nabd.Core.Interfaces.Repositories.Base;
using Nabd.Core.Interfaces.Repositories.Identity;
using Nabd.Core.Interfaces.Repositories.Feedback;
using Nabd.Core.Interfaces.Repositories.Profiles; // ضروري
using Nabd.Core.Interfaces.Repositories.Medical;
using Nabd.Core.Interfaces.Repositories.Operations;
using Nabd.Core.Interfaces.Repositories.Pharmacy;
using Nabd.Core.Interfaces.Repositories.AI;

namespace Nabd.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Identity
        IAppUserRepository Users { get; }
        IRefreshTokenRepository RefreshTokens { get; }

        // Profiles (✅ تم فك التعليق عنهم)
        IDoctorRepository Doctors { get; }
        IPatientRepository Patients { get; }
        IDoctorDocumentRepository DoctorDocuments { get; }

        // Medical
        IAppointmentRepository Appointments { get; }
        IConsultationRecordRepository ConsultationRecords { get; }
        IMedicalHistoryItemRepository MedicalHistoryItems { get; }

        // Operations
        IClinicBranchRepository ClinicBranches { get; }
        IDoctorScheduleRepository DoctorSchedules { get; }

        // Pharmacy
        IMedicationRepository Medications { get; }
        IPrescriptionRepository Prescriptions { get; }

        // Feedback
        IDoctorReviewRepository DoctorReviews { get; }

        // AI
        IAIDiagnosisLogRepository AIDiagnosisLogs { get; }

        // Core Actions
        Task<int> CompleteAsync(); // SaveChanges
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
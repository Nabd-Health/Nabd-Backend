using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Doctor
{
    // هذا DTO يحمل الإحصائيات الرئيسية لعرضها على لوحة تحكم الطبيب (Dashboard)
    public class DoctorDashboardStatsDto
    {
        // ==================================================
        // 1. User Context (للتخصيص)
        // ==================================================

        public required Guid DoctorId { get; set; }
        public required string DoctorName { get; set; }

        // ==================================================
        // 2. Schedule & Operational Metrics
        // ==================================================

        // عدد المواعيد المجدولة لليوم الحالي (Today Appointments)
        public int TodayAppointmentsCount { get; set; }

        // عدد المرضى في الانتظار حالياً (مهم لتنظيم الدور)
        public int WaitingPatientsCount { get; set; }

        // إجمالي عدد المرضى المسجلين لدى هذا الطبيب
        public int TotalPatientsCount { get; set; }

        // ==================================================
        // 3. Financial Metrics
        // ==================================================

        // إجمالي الإيرادات المتوقعة أو المحققة لليوم الحالي
        public decimal TodayRevenue { get; set; }

        // إجمالي الإيرادات المحققة لهذا الشهر (KPI)
        public decimal MonthToDateRevenue { get; set; }

        // ==================================================
        // 4. AI & Quality Metrics (Nabd Value)
        // ==================================================

        // عدد الاستشارات التي تم فيها استخدام المساعد الذكي (AI)
        public int AIConsultationsCount { get; set; }

        // معدل تقييم الطبيب (يستخدم في Dashboard للمتابعة)
        public double AverageRating { get; set; }

        // عدد الروشتات التي تم كتابتها ولكن لم يتم إكمالها أو إرسالها (Pending Tasks)
        public int PendingPrescriptionsCount { get; set; }

        // ==================================================
        // 5. Future/Audit Metrics
        // ==================================================

        // معدل الإلغاء (مهم لتحليل الأداء والتخطيط للمستقبل)
        public double CancellationRate { get; set; } = 0.0;

        // رسالة تنبيه من النظام (مثال: "برجاء مراجعة ترخيصك")
        public string? SystemAlertMessage { get; set; }
    }
}
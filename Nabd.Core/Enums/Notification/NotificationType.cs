using System.ComponentModel;

namespace Nabd.Core.Enums
{
    public enum NotificationType
    {
        [Description("إشعار عام")]
        General = 0,

        // ==========================================
        // 1. Appointment Events (المواعيد)
        // ==========================================
        [Description("حجز موعد جديد")]
        AppointmentBooking = 1,

        [Description("تأكيد موعد")]
        AppointmentConfirmation = 2,

        [Description("إلغاء موعد")]
        AppointmentCancelled = 3,

        [Description("تذكير بموعد")]
        AppointmentReminder = 4, // (مهم جداً لتقليل الـ No-Show)

        [Description("تغيير موعد")]
        AppointmentRescheduled = 5,

        // ==========================================
        // 2. Medical & AI Events (الجانب الطبي)
        // ==========================================
        [Description("تنبيه طبي هام")]
        MedicalAlert = 6,        // (يستخدمه الـ AI لو اكتشف خطر)

        [Description("روشتة جديدة")]
        NewPrescription = 7,

        [Description("نتائج تحاليل")]
        LabResultReady = 8,      // (للمستقبل عند ربط المعمل)

        [Description("تحديث الملف الطبي")]
        MedicalRecordUpdate = 9,

        // ==========================================
        // 3. Account & System (النظام)
        // ==========================================
        [Description("تفعيل الحساب")]
        AccountVerification = 10,

        [Description("تحديث أمني")]
        SecurityAlert = 11,      // (لو تم الدخول من جهاز غريب)

        [Description("تحديث في النظام")]
        SystemUpdate = 12
    }
}
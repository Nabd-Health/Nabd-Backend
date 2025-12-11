using System.ComponentModel;

namespace Nabd.Core.Enums
{
    public enum NotificationType
    {
        [Description("إشعار عام")]
        General = 0,

        // ==========================================
        // 1. Appointment Events 
        // ==========================================
        [Description("حجز موعد جديد")]
        AppointmentBooking = 1,

        [Description("تأكيد موعد")]
        AppointmentConfirmation = 2,

        [Description("إلغاء موعد")]
        AppointmentCancelled = 3,

        [Description("تذكير بموعد")]
        AppointmentReminder = 4, 

        [Description("تغيير موعد")]
        AppointmentRescheduled = 5,

        // ==========================================
        // 2. Medical & AI Events 
        // ==========================================
        [Description("تنبيه طبي هام")]
        MedicalAlert = 6,        

        [Description("روشتة جديدة")]
        NewPrescription = 7,

        [Description("نتائج تحاليل")]
        LabResultReady = 8,      

        [Description("تحديث الملف الطبي")]
        MedicalRecordUpdate = 9,

        // ==========================================
        // 3. Account & System
        // ==========================================
        [Description("تفعيل الحساب")]
        AccountVerification = 10,

        [Description("تحديث أمني")]
        SecurityAlert = 11,      

        [Description("تحديث في النظام")]
        SystemUpdate = 12
    }
}
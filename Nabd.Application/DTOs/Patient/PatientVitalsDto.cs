using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Patient
{
    // هذا DTO يمثل لقطة (Snapshot) للعلاقات الحيوية للمريض في وقت زيارة معينة
    public class PatientVitalsDto
    {
        // ==================================================
        // 1. Time Context
        // ==================================================

        [Required]
        // متى تم أخذ القراءات؟ (يربط القراءة بالزيارة)
        public required DateTime MeasurementDate { get; set; }

        // ==================================================
        // 2. Core Vitals (علامات حيوية)
        // ==================================================

        // الحرارة (قد تكون null)
        public double? Temperature { get; set; }

        // ضغط الدم الانقباضي
        public int? SystolicBloodPressure { get; set; }

        // ضغط الدم الانبساطي
        public int? DiastolicBloodPressure { get; set; }

        // معدل ضربات القلب (BPM)
        public int? HeartRate { get; set; }

        // معدل التنفس
        public int? RespiratoryRate { get; set; }

        // نسبة الأكسجين (SPO2)
        public double? OxygenSaturation { get; set; }

        // ==================================================
        // 3. Anthropometrics (القياسات الجسمانية)
        // ==================================================

        // الوزن وقت الزيارة (مهم لحساب الجرعات)
        public double? WeightAtVisit { get; set; }

        // الطول
        public double? Height { get; set; }

        // ==================================================
        // 4. Status (مؤشر للـ Frontend)
        // ==================================================

        // مثال: 'Fever', 'Normal', 'Tachycardia' (يمكن إضافتها بواسطة لوجيك في الـ Service)
        public string? StatusFlag { get; set; }

        // هذا الحقل يمكن أن يستخدمه الـ AI Model كـ input أيضاً (لتحديد الخطورة)
    }
}
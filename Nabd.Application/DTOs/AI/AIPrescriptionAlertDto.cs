using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.AI
{
    /// <summary>
    /// النتيجة النهائية القادمة من موديل تحليل الروشتات (Prescription Analyzer Model).
    /// يعمل كصمام أمان (Safety Valve) قبل اعتماد الروشتة.
    /// </summary>
    public class AIPrescriptionAnalysisResultDto
    {
        // ==========================================
        // 1. Overall Status (التقييم العام)
        // ==========================================

        /// <summary>
        /// هل الروشتة آمنة تماماً؟ (True = Green Light).
        /// </summary>
        public bool IsSafe { get; set; }

        /// <summary>
        /// مستوى الخطورة العام للروشتة (Safe, Moderate Risk, High Risk, Critical).
        /// يساعد الـ UI في تلوين التحذير (أخضر، أصفر، أحمر).
        /// </summary>
        public string OverallRiskLevel { get; set; } = "Safe";

        // ==========================================
        // 2. MLOps Metadata (بيانات الأداء)
        // ==========================================

        public required string ModelName { get; set; }
        public required string ModelVersion { get; set; }
        public double AnalysisTimeMs { get; set; } // زمن التحليل

        // ==========================================
        // 3. The Findings (النتائج والتحذيرات)
        // ==========================================

        /// <summary>
        /// قائمة التنبيهات والمشاكل المكتشفة (Interactions, Allergies).
        /// </summary>
        public List<PrescriptionAlertDto> Alerts { get; set; } = new();

        /// <summary>
        /// التوصيات الذكية (إضافة تحاليل، تغيير جرعة).
        /// </summary>
        public List<SmartRecommendationDto> Recommendations { get; set; } = new();
    }

    /// <summary>
    /// تفاصيل التحذير (المشكلة).
    /// </summary>
    public class PrescriptionAlertDto
    {
        /// <summary>
        /// نوع التحذير (Drug-Drug Interaction, Drug-Allergy, Drug-Disease, Duplicate Therapy).
        /// </summary>
        public required string AlertType { get; set; }

        /// <summary>
        /// درجة الخطورة (Low, Moderate, Major, Contraindicated).
        /// </summary>
        public required string Severity { get; set; }

        /// <summary>
        /// عنوان التحذير (مثال: "Interaction detected between Aspirin and Warfarin").
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// شرح مفصل للمخاطر الطبية.
        /// مثال: "الجمع بين هذين الدوائين يزيد من خطر النزيف بشكل ملحوظ."
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// العناصر المتعارضة (أسماء الأدوية أو الأمراض).
        /// مثال: ["Aspirin", "Warfarin"] أو ["Penicillin", "Patient Allergy"]
        /// </summary>
        public List<string> ConflictingElements { get; set; } = new();

        /// <summary>
        /// (ميزة احترافية) المرجع العلمي أو المصدر (Evidence Base).
        /// </summary>
        public string? ReferenceSource { get; set; }
    }

    /// <summary>
    /// تفاصيل التوصية (الحل المقترح).
    /// </summary>
    public class SmartRecommendationDto
    {
        /// <summary>
        /// نوع التوصية (Missing Lab Test, Dosage Adjustment, Alternative Drug).
        /// </summary>
        public required string Type { get; set; }

        /// <summary>
        /// نص التوصية.
        /// مثال: "المريض يأخذ Metformin لفترة طويلة، يُنصح بفحص Vitamin B12".
        /// </summary>
        public required string Suggestion { get; set; }

        /// <summary>
        /// السبب (Explainability).
        /// </summary>
        public string? Reasoning { get; set; }

        /// <summary>
        /// (اختياري) كود الإجراء المقترح (مثال: كود تحليل B12) لتسهيل إضافته بضغطة زر.
        /// </summary>
        public string? SuggestedActionCode { get; set; }
    }
}
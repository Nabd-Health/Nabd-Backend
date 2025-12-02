using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.AI
{
    /// <summary>
    /// النتيجة النهائية القادمة من موديل التشخيص (Diagnosis Aid Model).
    /// يحتوي على التوقعات، الأسباب، وميتاداتا عن أداء الموديل.
    /// </summary>
    public class AIDiagnosisResultDto
    {
        // ==========================================
        // 1. Context (سياق العملية)
        // ==========================================

        /// <summary>
        /// معرف الكشف الطبي الذي تم تحليله.
        /// </summary>
        public Guid ConsultationRecordId { get; set; }

        /// <summary>
        /// وقت التحليل (Timestamp).
        /// </summary>
        public DateTime AnalysisDate { get; set; } = DateTime.UtcNow;

        // ==========================================
        // 2. Model Metadata (بيانات الموديل - MLOps)
        // ==========================================

        /// <summary>
        /// اسم الموديل المستخدم (مثال: "Nabd-BERT-Ar-Medical").
        /// </summary>
        public required string ModelName { get; set; }

        /// <summary>
        /// إصدار الموديل (مثال: "v2.1.0"). مهم جداً للتتبع.
        /// </summary>
        public required string ModelVersion { get; set; }

        /// <summary>
        /// الوقت المستغرق في التحليل بالمللي ثانية (لقياس الأداء).
        /// </summary>
        public double ProcessingDurationMs { get; set; }

        // ==========================================
        // 3. Predictions (التوقعات)
        // ==========================================

        /// <summary>
        /// قائمة التشخيصات المحتملة مرتبة تنازلياً حسب الثقة.
        /// </summary>
        public List<DiagnosisPredictionDto> Predictions { get; set; } = new();

        /// <summary>
        /// هل تم اكتشاف أي "علامات حمراء" (Red Flags) تستدعي انتباهاً فورياً؟
        /// </summary>
        public bool HasCriticalFlag { get; set; }
    }

    /// <summary>
    /// تفاصيل تشخيص واحد محتمل.
    /// </summary>
    public class DiagnosisPredictionDto
    {
        /// <summary>
        /// اسم التشخيص (مثال: "Acute Bronchitis").
        /// </summary>
        public required string DiseaseName { get; set; }

        /// <summary>
        /// الكود العالمي للمرض (ICD-10) - (ميزة احترافية للربط مع التأمين).
        /// مثال: "J20.9"
        /// </summary>
        public string? ICD10Code { get; set; }

        /// <summary>
        /// نسبة الثقة (من 0.0 إلى 1.0).
        /// مثال: 0.85 يعني 85%.
        /// </summary>
        public double ConfidenceScore { get; set; }

        /// <summary>
        /// درجة الخطورة المتوقعة لهذا التشخيص (Low, Moderate, High, Critical).
        /// </summary>
        public string SeverityLevel { get; set; } = "Unknown";

        // ==========================================
        // Explainable AI (XAI) Features
        // ==========================================

        /// <summary>
        /// السبب: لماذا اختار الـ AI هذا التشخيص؟
        /// مثال: "Matches symptoms: High Fever, Dry Cough, Wheezing".
        /// </summary>
        public string? Reasoning { get; set; }

        /// <summary>
        /// الأعراض التي كانت مفقودة ولكنها عادة ترتبط بهذا المرض (تلميح للطبيب ليسأل عنها).
        /// مثال: ["Shortness of breath", "Chest pain"]
        /// </summary>
        public List<string>? MissingSymptomsAlert { get; set; } = new();

        /// <summary>
        /// إجراءات مقترحة (Next Best Action).
        /// مثال: "Order Chest X-Ray".
        /// </summary>
        public string? RecommendedAction { get; set; }
    }
}
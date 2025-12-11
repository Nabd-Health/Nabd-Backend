using Nabd.Application.DTOs;
using Nabd.Application.DTOs.AI;
using Nabd.Application.Interfaces;
using Nabd.Core.DTOs;
using Nabd.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Nabd.Application.Services.AI
{
    public class MockAIService : IAIService
    {
      
        private readonly List<MockDisease> _knowledgeBase = new()
        {
            new MockDisease
            {
                Name = "الأنفلونزا الموسمية (Seasonal Influenza)",
                Keywords = new[] { "سخونية", "حرارة", "تكسير", "fever", "flu", "body ache" },
                Recommendation = "راحة تامة، شرب سوائل دافئة، باراسيتامول عند اللزوم.",
                IsCritical = false
            },
            new MockDisease
            {
                Name = "نزلات البرد (Common Cold)",
                Keywords = new[] { "رشح", "زكام", "عطس", "cold", "runny nose", "sneeze" },
                Recommendation = "فيتامين سي، مضادات الهيستامين، الراحة.",
                IsCritical = false
            },
            new MockDisease
            {
                Name = "اشتباه في ذبحة صدرية (Angina Pectoris)",
                Keywords = new[] { "ألم صدر", "نغزة", "ذراع", "chest pain", "heart", "arm pain" },
                Recommendation = "تخطيط قلب (ECG) فوراً، قياس إنزيمات القلب.",
                IsCritical = true 
            },
            new MockDisease
            {
                Name = "التهاب المعدة / القولون (Gastritis/IBS)",
                Keywords = new[] { "مغص", "بطن", "انتفاخ", "stomach", "pain", "bloating" },
                Recommendation = "تنظيم الأكل، تجنب المسبكات، دواء مهضم.",
                IsCritical = false
            },
             new MockDisease
            {
                Name = "صداع نصفي (Migraine)",
                Keywords = new[] { "صداع", "دوخة", "headache", "dizzy", "migraine" },
                Recommendation = "قياس ضغط الدم، مسكنات، الابتعاد عن الضوء والضوضاء.",
                IsCritical = false
            }
        };

        public async Task<AIDiagnosisResponse> AnalyzeSymptomsAsync(AIDiagnosisRequest request)
        {
            // 1. (Processing Time)
            await Task.Delay(1500);

            var response = new AIDiagnosisResponse
            {
                RequestId = Guid.NewGuid().ToString(),
                AnalyzedAt = DateTime.UtcNow,
                Predictions = new List<PredictedDiseaseDto>()
            };

            var input = request.Symptoms.ToLower();

            // 2. (Mock Engine)
            foreach (var disease in _knowledgeBase)
            {

                int matchCount = disease.Keywords.Count(k => input.Contains(k));

                if (matchCount > 0)
                {
                    
                    double score = Math.Min(0.95, 0.30 + (matchCount * 0.15));

                    response.Predictions.Add(new PredictedDiseaseDto
                    {
                        DiseaseName = disease.Name,
                        ConfidenceScore = Math.Round(score, 2), 
                        Recommendation = disease.Recommendation,
                        IsCritical = disease.IsCritical
                    });
                }
            }

            // 3. ترتيب النتائج حسب الأقوى
            response.Predictions = response.Predictions
                .OrderByDescending(p => p.ConfidenceScore)
                .ToList();

            // 4. Fallback (لو مفيش ولا مرض طابق الأعراض)
            if (!response.Predictions.Any())
            {
                response.Predictions.Add(new PredictedDiseaseDto
                {
                    DiseaseName = "حالة غير محددة (Undiagnosed)",
                    ConfidenceScore = 0.50,
                    Recommendation = "يرجى إجراء الفحص السريري وطلب تحاليل شاملة (CBC).",
                    IsCritical = false
                });
            }

            return response;
        }

        public async Task<bool> SubmitFeedbackAsync(AIFeedbackRequest request)
        {
         
            await Task.Delay(500);
            return true;
        }

   
        private class MockDisease
        {
            public string Name { get; set; } = string.Empty;
            public string[] Keywords { get; set; } = Array.Empty<string>();
            public string Recommendation { get; set; } = string.Empty;
            public bool IsCritical { get; set; }
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nabd.Application.DTOs.AI; 
using Nabd.Application.Interfaces; 
using System.Threading.Tasks;

namespace Nabd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")] 
    public class AIDiagnosisController : ControllerBase
    {
        private readonly IAIService _aiService;

        public AIDiagnosisController(IAIService aiService)
        {
            _aiService = aiService;
        }

        // ==========================================
        // 1.(Core Feature)
        // ==========================================

        /// <summary>
        /// تحليل الأعراض واقتراح تشخيص (AI Powered).
        /// </summary>
        /// <remarks>
        /// يرسل الأعراض والعلامات الحيوية إلى نموذج الذكاء الاصطناعي ويعيد قائمة بالأمراض المحتملة مرتبة حسب الثقة.
        /// </remarks>
        /// <param name="request">بيانات المريض والأعراض (مثل: "سخونية وكحة").</param>
        /// <returns>قائمة التوقعات (Predictions) مع النصائح ونسبة الثقة.</returns>
        [HttpPost("analyze")]
        [ProducesResponseType(typeof(AIDiagnosisResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AnalyzeSymptoms([FromBody] AIDiagnosisRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            var result = await _aiService.AnalyzeSymptomsAsync(request);

            return Ok(result);
        }

        // ==========================================
        // 2. التعلم والتحسين (Feedback Loop)
        // ==========================================

        /// <summary>
        /// إرسال تصحيح الطبيب للتشخيص (للتعلم والتحسين المستمر).
        /// </summary>
        /// <remarks>
        /// يستخدم لتسجيل ما إذا كان اقتراح الـ AI مفيداً أم لا، وما هو التشخيص الصحيح الذي اعتمده الطبيب.
        /// </remarks>
        /// <param name="request">بيانات التقييم (RequestId, WasCorrect, CorrectDiagnosis).</param>
        [HttpPost("feedback")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SubmitFeedback([FromBody] AIFeedbackRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _aiService.SubmitFeedbackAsync(request);

            if (!success)
                return BadRequest(new { Message = "فشل في حفظ التقييم، يرجى التأكد من RequestId." });

            return Ok(new { Message = "تم استلام التقييم بنجاح. شكراً لمساعدتك في تحسين نبض AI!" });
        }
    }
}
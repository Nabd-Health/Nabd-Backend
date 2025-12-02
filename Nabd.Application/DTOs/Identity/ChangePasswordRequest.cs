using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Identity
{
    public class ChangePasswordRequest
    {
        // 1. كلمة المرور الحالية (للتحقق من هوية المستخدم قبل التغيير)
        [Required(ErrorMessage = "كلمة المرور الحالية مطلوبة.")]
        public required string CurrentPassword { get; set; }

        // 2. كلمة المرور الجديدة
        [Required(ErrorMessage = "كلمة المرور الجديدة مطلوبة.")]
        [MinLength(8, ErrorMessage = "يجب أن لا تقل كلمة المرور عن 8 أحرف.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "كلمة المرور يجب أن تحتوي على حرف كبير، حرف صغير، رقم، ورمز خاص.")]
        public required string NewPassword { get; set; }

        // 3. تأكيد كلمة المرور الجديدة (لمنع الأخطاء المطبعية)
        [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب.")]
        [Compare(nameof(NewPassword), ErrorMessage = "كلمة المرور وتأكيدها غير متطابقين.")]
        public required string ConfirmNewPassword { get; set; }
    }
}
using FluentValidation;
using Nabd.Application.DTOs.Identity;
using System.Text.RegularExpressions;

namespace Nabd.Application.Validators.Identity
{
    public class RegisterDoctorDtoValidator : AbstractValidator<RegisterDoctorDto>
    {
        public RegisterDoctorDtoValidator()
        {
            // ==========================================
            // 1. Identity Info (بيانات الحساب)
            // ==========================================

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("الاسم الأول مطلوب.")
                .Length(2, 50).WithMessage("الاسم يجب أن يكون بين 2 و 50 حرفاً.")
                .Matches(@"^[\u0600-\u06FFa-zA-Z\s]+$").WithMessage("الاسم يجب أن يحتوي على أحرف فقط.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("الاسم الأخير مطلوب.")
                .Length(2, 50).WithMessage("الاسم يجب أن يكون بين 2 و 50 حرفاً.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب.")
                .EmailAddress().WithMessage("صيغة البريد الإلكتروني غير صحيحة.");

            // كلمة المرور: (8 حروف، حرف كبير، حرف صغير، رقم، رمز خاص)
            // لتتوافق مع إعدادات IdentityExtensions التي كتبناها
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("كلمة المرور مطلوبة.")
                .MinimumLength(8).WithMessage("كلمة المرور يجب أن لا تقل عن 8 خانات.")
                .Matches("[A-Z]").WithMessage("يجب أن تحتوي على حرف كبير واحد على الأقل.")
                .Matches("[a-z]").WithMessage("يجب أن تحتوي على حرف صغير واحد على الأقل.")
                .Matches("[0-9]").WithMessage("يجب أن تحتوي على رقم واحد على الأقل.")
                .Matches("[^a-zA-Z0-9]").WithMessage("يجب أن تحتوي على رمز خاص (مثل @, #, $).");

            // رقم الهاتف المصري (يبدأ بـ 010, 011, 012, 015 ويتكون من 11 رقم)
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب.")
                .Matches(@"^01[0125][0-9]{8}$").WithMessage("رقم الهاتف غير صحيح (يجب أن يكون رقم موبايل مصري مكون من 11 خانة).");

            // ==========================================
            // 2. Professional Info (البيانات المهنية)
            // ==========================================

            RuleFor(x => x.Specialization)
                .IsInEnum().WithMessage("التخصص المختار غير صالح.");

            RuleFor(x => x.MedicalLicenseNumber)
                .NotEmpty().WithMessage("رقم الترخيص الطبي مطلوب.")
                .MaximumLength(50).WithMessage("رقم الترخيص طويل جداً.");

            RuleFor(x => x.YearsOfExperience)
                .GreaterThanOrEqualTo(0).WithMessage("سنوات الخبرة لا يمكن أن تكون بالسالب.")
                .LessThanOrEqualTo(70).WithMessage("سنوات الخبرة غير منطقية.");

            RuleFor(x => x.Bio)
                .MaximumLength(1000).WithMessage("النبذة التعريفية لا يجب أن تتجاوز 1000 حرف.");

            // ==========================================
            // 3. Clinic Info (بيانات العيادة المبدئية)
            // ==========================================

            RuleFor(x => x.ClinicName)
                .NotEmpty().WithMessage("اسم العيادة مطلوب.")
                .MaximumLength(100).WithMessage("اسم العيادة طويل جداً.");

            RuleFor(x => x.ClinicAddress)
                .NotEmpty().WithMessage("عنوان العيادة مطلوب.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("المدينة مطلوبة.");

            RuleFor(x => x.ConsultationFee)
                .GreaterThan(0).WithMessage("سعر الكشف يجب أن يكون أكبر من صفر.")
                .LessThanOrEqualTo(10000).WithMessage("سعر الكشف يتجاوز الحد المسموح.");
        }
    }
}
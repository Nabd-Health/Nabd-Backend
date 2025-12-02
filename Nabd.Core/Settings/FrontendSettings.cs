namespace Nabd.Core.Settings
{
    // إعدادات الروابط الأمامية (تُستخدم لرد المستخدم بعد عملية الدفع)
    public class FrontendSettings
    {
        public string BaseUrl { get; set; } = string.Empty;

        // [Nabd]: يجب أن ترجع صفحة الدفع إلى لوحة التحكم أو صفحة الموعد
        public string PaymentSuccessUrl { get; set; } = "/booking/success";
        public string PaymentFailedUrl { get; set; } = "/booking/failed";
    }
}
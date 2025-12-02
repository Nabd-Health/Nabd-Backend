using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Shared.Configurations
{
    // هذا الكلاس يمثل إعدادات الدخول الموحد (Single Sign-On) عبر جهات خارجية
    public class OAuthSettings
    {
        // إعدادات Google (كخدمة أساسية للدخول الاجتماعي)
        public GoogleOAuthSettings Google { get; set; } = new();

        // يمكن إضافة إعدادات لـ Facebook, Apple, وغيرها في المستقبل.
    }

    // إعدادات خاصة بتطبيق Google OAuth
    public class GoogleOAuthSettings
    {
        // هل تم تفعيل الدخول عبر Google؟
        public bool Enabled { get; set; } = false;

        // مفتاح التطبيق العام من Google Console
        public string ClientId { get; set; } = string.Empty;

        // مفتاح التطبيق السري
        public string ClientSecret { get; set; } = string.Empty;

        // المسار الذي ستعود إليه Google بعد المصادقة
        public string CallbackPath { get; set; } = "/api/auth/google-callback";

        // النطاقات المطلوبة من Google (لجلب الاسم، البروفايل، والإيميل)
        public string[] Scopes { get; set; } = new[]
        {
            "openid",
            "profile",
            "email"
        };
    }
}
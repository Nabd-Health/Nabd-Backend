namespace Nabd.Shared.Configurations
{
    // إعدادات خدمة Cloudinary لتخزين وإدارة الصور والملفات
    public class CloudinarySettings
    {
        // اسم السحابة (من Dashboard Cloudinary)
        public string CloudName { get; set; } = string.Empty;

        // مفتاح الـ API العام
        public string ApiKey { get; set; } = string.Empty;

        // المفتاح السري (يجب عدم مشاركته)
        public string ApiSecret { get; set; } = string.Empty;
    }
}
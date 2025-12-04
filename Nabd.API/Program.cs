using Nabd.API.Extensions; // عشان AddApplicationServices
using Nabd.Infrastructure.Extensions; // عشان AddIdentityServices (نقلناها هنا)
using Nabd.Shared.Extensions; // عشان Authentication, Cors, Seeding
using Nabd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ====================================================
// 1. تسجيل الخدمات (Services Container)
// ====================================================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// A. خدمات التطبيق والبنية التحتية (Core & Infra & Application)
// ✅ التعديل 1: مررنا Configuration لأن الدالة بتحتاجها لضبط الـ DbContext
builder.Services.AddApplicationServices(builder.Configuration);

// B. خدمات الهوية (Identity)
// ✅ التعديل 2: الاسم الصحيح حسب الملف اللي عملناه في Infrastructure
builder.Services.AddIdentityServices();

// C. إعدادات الأمان والتواصل (Shared)
// ✅ التعديل 3: استخدام دوال Shared اللي جهزناها
builder.Services.AddJwtAuthentication(builder.Configuration);
// builder.Services.AddAuthorizationPolicies(); // (مؤجل حالياً)
builder.Services.AddCorsConfiguration(builder.Configuration);
// builder.Services.AddEmailServices(builder.Configuration); // (مؤجل لو مفيش إعدادات SMTP)

var app = builder.Build();

// ====================================================
// 2. إعدادات الـ Middleware (HTTP Pipeline)
// ====================================================

// ✅ تهيئة قاعدة البيانات (Migrations & Seeding) أوتوماتيكياً عند البدء
// ده الكود السحري اللي بيعمل Update-Database لوحده
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // 1. تطبيق الميجريشن (إنشاء الجداول)
        var context = services.GetRequiredService<NabdDbContext>();
        await context.Database.MigrateAsync();

        // 2. ملء البيانات الأولية (Seeding)
        await DatabaseSeederExtension.SeedDatabaseAsync(app);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migration/seeding.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// تفعيل الـ CORS
app.UseCors("NabdCorsPolicy");

// ترتيب الـ Auth مهم جداً
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
using Nabd.API.Extensions;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ===========================================
// 1. تسجيل الخدمات (Services Registration)
// ===========================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 👇 توصيل الداتابيز والـ UoW (من ApplicationServiceExtensions)
// builder.Services.AddApplicationServices(builder.Configuration);

// 👇 توصيل خدمات الـ JWT والـ Auth (من IdentityServiceExtensions)
builder.Services.AddIdentityServices(builder.Configuration);

// 👇 سياسة CORS: السماح لـ React (localhost:3000) بالتواصل مع الـ API
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:3000"); // هذا هو المنفذ الافتراضي لـ React/Vite
    });
});

var app = builder.Build();

// ===========================================
// 2. تفعيل الـ Middleware (Pipeline Configuration)
// ===========================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 👇 تفعيل سياسة CORS
app.UseCors("CorsPolicy");

// 👇 تفعيل الـ Authentication (مهم أن يكون قبل الـ Authorization)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

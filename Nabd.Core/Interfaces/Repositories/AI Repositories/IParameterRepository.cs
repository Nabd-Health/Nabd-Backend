using System.Threading.Tasks;
using System.Collections.Generic;
using Nabd.Core.Entities.System; // نفترض إنشاء Entity جديدة اسمها SystemParameter
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.System
{
    // هذا الريبوزيتوري مخصص للإعدادات الديناميكية والحساسة (مثل إعدادات الـ AI)
    public interface IParameterRepository : IGenericRepository<SystemParameter>
    {
        // ==========================================
        // I. Read Operations
        // ==========================================

        /// <summary>
        /// جلب قيمة باراميتر محدد بواسطة المفتاح (Key)
        /// </summary>
        Task<string?> GetValueByKeyAsync(string key);

        /// <summary>
        /// جلب جميع الباراميترات المتعلقة بموديل AI معين (مثال: 'AI.Diagnosis.ModelVersion')
        /// </summary>
        Task<Dictionary<string, string>> GetParametersByPrefixAsync(string prefix);

        // ==========================================
        // II. Write Operations
        // ==========================================

        /// <summary>
        /// تحديث قيمة باراميتر موجود أو إنشاءه إذا لم يكن موجوداً
        /// </summary>
        Task SetValueAsync(string key, string value, string? description = null);

        /// <summary>
        /// [مهم لـ AI] تحديث إصدار الموديل الجاري استخدامه
        /// </summary>
        Task UpdateModelVersionAsync(string modelName, string newVersion);
    }
}
using System.Threading.Tasks;
using Nabd.Core.Entities.System;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.System
{
    public interface IParameterRepository : IGenericRepository<SystemParameter>
    {
        // 1. جلب الإعداد بواسطة المفتاح (Key) بسرعة
        Task<SystemParameter?> GetByKeyAsync(string key);

        // 2. جلب القيمة فقط (String) مباشرة
        Task<string?> GetValueByKeyAsync(string key);
    }
}
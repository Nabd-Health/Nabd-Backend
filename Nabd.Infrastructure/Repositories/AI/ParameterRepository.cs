using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.System;
using Nabd.Core.Interfaces.Repositories.System;
using Nabd.Infrastructure.Data;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.System
{
    public class ParameterRepository : GenericRepository<SystemParameter>, IParameterRepository
    {
        public ParameterRepository(NabdDbContext context) : base(context)
        {
        }

        public async Task<SystemParameter?> GetByKeyAsync(string key)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => p.Key == key);
        }

        public async Task<string?> GetValueByKeyAsync(string key)
        {
            var param = await _dbSet
                .AsNoTracking() // أداء أسرع لأننا بنقرأ بس
                .FirstOrDefaultAsync(p => p.Key == key);

            return param?.Value;
        }
    }
}
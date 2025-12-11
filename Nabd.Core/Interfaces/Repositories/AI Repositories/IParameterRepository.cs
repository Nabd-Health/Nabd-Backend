using System.Threading.Tasks;
using Nabd.Core.Entities.System;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.System
{
    public interface IParameterRepository : IGenericRepository<SystemParameter>
    {

        Task<SystemParameter?> GetByKeyAsync(string key);

       
        Task<string?> GetValueByKeyAsync(string key);
    }
}
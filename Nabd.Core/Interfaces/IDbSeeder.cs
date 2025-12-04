using System.Threading.Tasks;

namespace Nabd.Core.Interfaces
{
    public interface IDbSeeder
    {
        Task SeedAsync();
    }
}
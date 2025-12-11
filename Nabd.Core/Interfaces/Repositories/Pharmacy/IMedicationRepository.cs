using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Pharmacy;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Pharmacy
{
    public interface IMedicationRepository : IGenericRepository<Medication>
    {
        
        Task<IEnumerable<Medication>> SearchAsync(string term);

       
        Task<Medication?> GetByBarcodeAsync(string barcode);

       
        Task<IEnumerable<Medication>> GetMostPrescribedAsync(int topCount = 10);

        
        Task<IEnumerable<Medication>> GetActiveMedicationsAsync();
    }
}
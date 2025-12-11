using System.Threading.Tasks;
using Nabd.Application.DTOs.AI; 

namespace Nabd.Application.Interfaces
{
    public interface IAIService
    {
      
        Task<AIDiagnosisResponse> AnalyzeSymptomsAsync(AIDiagnosisRequest request);


        Task<bool> SubmitFeedbackAsync(AIFeedbackRequest request);
    }
}
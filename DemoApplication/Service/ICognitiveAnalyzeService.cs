using System;
using System.Threading.Tasks;
using DemoApplication.Models;

namespace DemoApplication.Service
{
    public interface ICognitiveAnalyzeService
    {
        Task<CognitiveResponseModel> GetMetadatasFromAzureCognitive(byte[] imageDatas);
    }
}

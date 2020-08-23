using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace DemoApplication.Service
{
    public class CognitiveAnalyzeService : ICognitiveAnalyzeService
    {
        private readonly IConfiguration _config;
        private string _apiKey;
        private string _apiUrlBase;

        public CognitiveAnalyzeService(IConfiguration config)
        {
            _config = config;
            _apiKey = _config.GetValue<string>("COMPUTER_VISION_SUBSCRIPTION_KEY");
            _apiUrlBase = _config.GetValue<string>("COMPUTER_VISION_SUBSCRIPTION_KEY");
        }

        public async Task<string> GetMetadatasFromAzureCognitive(byte[] imageDatas)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);

                string requestParameters = "visualFeatures=Categories,Description,Color";

                // Assemble the URI for the REST API method.
                string uri = _apiUrlBase + "?" + requestParameters;

                HttpResponseMessage response;

                using (ByteArrayContent content = new ByteArrayContent(imageDatas))
                {
                    content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");

                    // Asynchronously call the REST API method.
                    response = await httpClient.PostAsync(uri, content);
                }

                // Asynchronously get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();
                dynamic jToken = JToken.Parse(contentString);

                return jToken.Categories.ToList();
            }
        }

    }
}

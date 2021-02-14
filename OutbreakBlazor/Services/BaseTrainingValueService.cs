using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OutbreakModels.Models;

namespace OutbreakBlazor.Services
{
    public class BaseTrainingValueService : IBaseTrainingValueService
    {
        private readonly HttpClient _httpClient;

        public BaseTrainingValueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<BaseTrainingValue>> GetTrainingValues()
        {
            return await _httpClient.GetJsonAsync<BaseTrainingValue[]>("/api/BaseTrainingValues");
        }
    }
}

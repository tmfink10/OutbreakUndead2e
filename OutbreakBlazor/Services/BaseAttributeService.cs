using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OutbreakModels.Models;

namespace OutbreakBlazor.Services
{
    public class BaseAttributeService : IBaseAttributeService
    {
        private readonly HttpClient _httpClient;

        public BaseAttributeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<BaseAttribute>> GetBaseAttributes()
        {
            return await _httpClient.GetJsonAsync<BaseAttribute[]>("/api/BaseAttributes");
        }
        public async Task<BaseAttribute> GetBaseAttribute(int id)
        {
            return await _httpClient.GetJsonAsync<BaseAttribute>($"/api/PlayerAttributes/{id}");
        }
    }
}

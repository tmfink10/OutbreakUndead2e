using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OutbreakModels.Models;

namespace OutbreakBlazor.Services
{
    public class BaseAbilityService : IBaseAbilityService
    {
        private readonly HttpClient _httpClient;

        public BaseAbilityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<BaseAbility>> GetBaseAbilities()
        {
            return await _httpClient.GetJsonAsync<BaseAbility[]>("/api/BaseAbilities");
        }

        public async Task<BaseAbility> GetBaseAbility(int id)
        {
            return await _httpClient.GetJsonAsync<BaseAbility>($"/api/BaseAbilities/{id}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OutbreakModels.Models;

namespace OutbreakBlazor.Services
{
    public class PlayerAttributeService : IPlayerAttributeService
    {
        private readonly HttpClient _httpClient;

        public PlayerAttributeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PlayerAttribute> GetPlayerAttribute(int id)
        {
            return await _httpClient.GetJsonAsync<PlayerAttribute>($"/api/PlayerAttributes/{id}");
        }

        public async Task<IEnumerable<PlayerAttribute>> GetPlayerAttributes()
        {
            return await _httpClient.GetJsonAsync<PlayerAttribute[]>("/api/PlayerAttributes");
        }

        public async Task<PlayerAttribute> CreatePlayerAttribute(PlayerAttribute playerAttribute)
        {
            return await _httpClient.PostJsonAsync<PlayerAttribute>("/api/PlayerAttributes", playerAttribute);
        }

        public async Task<PlayerAttribute> UpdatePlayerAttribute(int id, PlayerAttribute playerAttribute)
        {
            return await _httpClient.PutJsonAsync<PlayerAttribute>($"/api/PlayerAttributes/{id}", playerAttribute);
        }
    }

}

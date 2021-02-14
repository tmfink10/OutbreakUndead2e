using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OutbreakModels.Models;

namespace OutbreakBlazor.Services
{
    public class PlayerAbilityService : IPlayerAbilityService
    {
        private readonly HttpClient _httpClient;

        public PlayerAbilityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PlayerAbility> GetPlayerAbility(int id)
        {
            return await _httpClient.GetJsonAsync<PlayerAbility>($"/api/PlayerAbilities/{id}");
        }

        public async Task<IEnumerable<PlayerAbility>> GetPlayerAbilities()
        {
            return await _httpClient.GetJsonAsync<PlayerAbility[]>("/api/PlayerAbilities");
        }

        public async Task<PlayerAbility> CreatePlayerAbility(PlayerAbility playerAbility)
        {
            return await _httpClient.PostJsonAsync<PlayerAbility>("/api/PlayerAbilities", playerAbility);
        }

        public async Task<PlayerAbility> UpdatePlayerAbility(int id, PlayerAbility playerAbility)
        {
            return await _httpClient.PutJsonAsync<PlayerAbility>($"/api/PlayerAbilities/{id}", playerAbility);
        }
    }

}

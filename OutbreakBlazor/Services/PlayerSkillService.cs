using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OutbreakModels.Models;

namespace OutbreakBlazor.Services
{
    public class PlayerSkillService : IPlayerSkillService
    {
        private readonly HttpClient _httpClient;

        public PlayerSkillService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PlayerSkill> GetPlayerSkill(int id)
        {
            return await _httpClient.GetJsonAsync<PlayerSkill>($"/api/PlayerSkills/{id}");
        }

        public async Task<IEnumerable<PlayerSkill>> GetPlayerSkills()
        {
            return await _httpClient.GetJsonAsync<PlayerSkill[]>("/api/PlayerSkills");
        }

        public async Task<PlayerSkill> CreatePlayerSkill(PlayerSkill playerSkill)
        {
            return await _httpClient.PostJsonAsync<PlayerSkill>("/api/PlayerSkills", playerSkill);
        }

        public async Task<PlayerSkill> UpdatePlayerSkill(int id, PlayerSkill playerSkill)
        {
            return await _httpClient.PutJsonAsync<PlayerSkill>($"/api/PlayerSkills/{id}", playerSkill);
        }
    }

}

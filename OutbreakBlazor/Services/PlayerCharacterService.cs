using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OutbreakModels.Models;

namespace OutbreakBlazor.Services
{
    public class PlayerCharacterService : IPlayerCharacterService
    {
        private readonly HttpClient _httpClient;

        public PlayerCharacterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PlayerCharacter>> GetPlayerCharacters()
        {
            return await _httpClient.GetJsonAsync<PlayerCharacter[]>("api/PlayerCharacters");
        }

        public async Task<PlayerCharacter> CreatePlayerCharacter(PlayerCharacter playerCharacter)
        {
            return await _httpClient.PostJsonAsync<PlayerCharacter>("/api/PlayerCharacters", playerCharacter);
        }

        public async Task<PlayerCharacter> UpdatePlayerCharacter(int id, PlayerCharacter playerCharacter)
        {
            return await _httpClient.PutJsonAsync<PlayerCharacter>($"/api/PlayerCharacters/{id}", playerCharacter);
        }
    }
}

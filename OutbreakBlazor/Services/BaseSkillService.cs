using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OutbreakModels.Models;

namespace OutbreakBlazor.Services
{
    public class BaseSkillService : IBaseSkillService
    {
        private readonly HttpClient _httpClient;

        public BaseSkillService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<BaseSkill>> GetBaseSkills()
        {
            return await _httpClient.GetJsonAsync<BaseSkill[]>("/api/BaseSkills");
        }
    }
}

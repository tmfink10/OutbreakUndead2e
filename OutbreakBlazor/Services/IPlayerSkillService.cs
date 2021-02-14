using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OutbreakModels.Models;

namespace OutbreakBlazor.Services
{
    public interface IPlayerSkillService
    {
        Task<IEnumerable<PlayerSkill>> GetPlayerSkills();
        Task<PlayerSkill> GetPlayerSkill(int Id);
        Task<PlayerSkill> UpdatePlayerSkill(int Id, PlayerSkill playerSkill);
        Task<PlayerSkill> CreatePlayerSkill(PlayerSkill playerSkill);
    }
}

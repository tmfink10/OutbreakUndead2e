using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OutbreakModels.Models;

namespace OutbreakBlazor.Services
{
    public interface IPlayerAbilityService
    {
        Task<IEnumerable<PlayerAbility>> GetPlayerAbilities();
        Task<PlayerAbility> GetPlayerAbility(int Id);
        Task<PlayerAbility> UpdatePlayerAbility(int Id, PlayerAbility playerAbility);
        Task<PlayerAbility> CreatePlayerAbility(PlayerAbility playerAbility);
    }
}

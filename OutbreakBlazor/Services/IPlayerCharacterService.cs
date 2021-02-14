using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OutbreakModels.Models;

namespace OutbreakBlazor.Services
{
    public interface IPlayerCharacterService
    {
        Task<IEnumerable<PlayerCharacter>> GetPlayerCharacters();
        Task<PlayerCharacter> UpdatePlayerCharacter(int id, PlayerCharacter playerCharacter);
        Task<PlayerCharacter> CreatePlayerCharacter(PlayerCharacter playerCharacter);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public interface IPlayerCharacterRepository
    {
        Task<IEnumerable<PlayerCharacter>> SearchPlayerCharacters(string search);
        Task<IEnumerable<PlayerCharacter>> GetPlayerCharacters();
        Task<PlayerCharacter> GetPlayerCharacter(int playerCharacterId);
        Task<PlayerCharacter> AddPlayerCharacter(PlayerCharacter playerCharacter);
        Task<PlayerCharacter> UpdatePlayerCharacter(PlayerCharacter playerCharacter);
        Task<PlayerCharacter> DeletePlayerCharacter(int playerCharacterId);
    }
}

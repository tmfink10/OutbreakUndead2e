using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public interface IPlayerAttributeRepository
    {
        Task<IEnumerable<PlayerAttribute>> SearchPlayerAttributes(string search);
        Task<IEnumerable<PlayerAttribute>> GetPlayerAttributes();
        Task<PlayerAttribute> GetPlayerAttribute(int playerAttributeId);
        Task<PlayerAttribute> AddPlayerAttribute(PlayerAttribute playerAttribute);
        Task<PlayerAttribute> UpdatePlayerAttribute(PlayerAttribute playerAttribute);
        Task<PlayerAttribute> DeletePlayerAttribute(int playerAttributeId);
    }
}

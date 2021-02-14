using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public interface IBaseAbilityRepository
    {
        Task<IEnumerable<BaseAbility>> SearchBaseAbilities(string search);
        Task<IEnumerable<BaseAbility>> GetBaseAbilities();
        Task<BaseAbility> GetBaseAbility(int baseAbilityId);
        Task<BaseAbility> AddBaseAbility(BaseAbility baseAbility);
        Task<BaseAbility> UpdateBaseAbility(BaseAbility baseAbility);
        Task<BaseAbility> DeleteBaseAbility(int baseAbilityId);
    }
}

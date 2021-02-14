using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OutbreakModels.Models;

namespace OutbreakBlazor.Services
{
    public interface IBaseAbilityService
    {
        Task<IEnumerable<BaseAbility>> GetBaseAbilities();
        Task<BaseAbility> GetBaseAbility(int id);
    }
}

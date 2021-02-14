using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public interface IBaseAttributeRepository
    {
        Task<IEnumerable<BaseAttribute>> SearchBaseAttributes(string search);
        Task<IEnumerable<BaseAttribute>> GetBaseAttributes();
        Task<BaseAttribute> GetBaseAttribute(int baseAttributeId);
        Task<BaseAttribute> AddBaseAttribute(BaseAttribute baseAttribute);
        Task<BaseAttribute> UpdateBaseAttribute(BaseAttribute baseAttribute);
        Task<BaseAttribute> DeleteBaseAttribute(int baseAttributeId);
    }
}

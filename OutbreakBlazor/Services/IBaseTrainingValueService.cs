using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OutbreakModels.Models;

namespace OutbreakBlazor.Services
{
    public interface IBaseTrainingValueService
    {
        Task<IEnumerable<BaseTrainingValue>> GetTrainingValues();
    }
}

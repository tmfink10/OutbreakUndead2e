using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public interface IBaseTrainingValueRepository
    {
        Task<IEnumerable<BaseTrainingValue>> SearchBaseTrainingValues(string search);
        Task<IEnumerable<BaseTrainingValue>> GetBaseTrainingValues();
        Task<BaseTrainingValue> GetBaseTrainingValue(int baseTrainingValueId);
        Task<BaseTrainingValue> AddBaseTrainingValue(BaseTrainingValue baseTrainingValue);
        Task<BaseTrainingValue> UpdateBaseTrainingValue(BaseTrainingValue baseTrainingValue);
        Task<BaseTrainingValue> DeleteBaseTrainingValue(int baseTrainingValueId);
    }
}

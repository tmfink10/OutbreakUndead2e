using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public class BaseTrainingValueRepository : IBaseTrainingValueRepository
    {
        private readonly AppDbContext _dbContext;

        public BaseTrainingValueRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BaseTrainingValue>> SearchBaseTrainingValues(string search)
        {
            IQueryable<BaseTrainingValue> query = _dbContext.BaseTrainingValues;

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                query = query.Where(t => t.Name.Contains(search) || t.Description.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<BaseTrainingValue>> GetBaseTrainingValues()
        {
            return await _dbContext.BaseTrainingValues.ToArrayAsync();
        }

        public async Task<BaseTrainingValue> GetBaseTrainingValue(int baseTrainingValueId)
        {
            return await _dbContext.BaseTrainingValues
                .FirstOrDefaultAsync(t => t.Id == baseTrainingValueId);
        }

        public async Task<BaseTrainingValue> AddBaseTrainingValue(BaseTrainingValue baseTrainingValue)
        {
            var result = await _dbContext.BaseTrainingValues.AddAsync(baseTrainingValue);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<BaseTrainingValue> UpdateBaseTrainingValue(BaseTrainingValue baseTrainingValue)
        {
            var result = await _dbContext.BaseTrainingValues
                .FirstOrDefaultAsync(t => t.Id == baseTrainingValue.Id);

            if (result != null)
            {
                result.Name = baseTrainingValue.Name;
                result.Description = baseTrainingValue.Description;
                result.HtmlDescription = baseTrainingValue.HtmlDescription;

                return result;
            }

            return null;
        }

        public async Task<BaseTrainingValue> DeleteBaseTrainingValue(int baseTrainingValueId)
        {
            var result = await _dbContext.BaseTrainingValues
                .FirstOrDefaultAsync(t => t.Id == baseTrainingValueId);

            if (result != null)
            {
                _dbContext.BaseTrainingValues.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }

}

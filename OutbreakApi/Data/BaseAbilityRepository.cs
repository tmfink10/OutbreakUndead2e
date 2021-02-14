using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public class BaseAbilityRepository : IBaseAbilityRepository
    {
        private readonly AppDbContext _dbContext;

        public BaseAbilityRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BaseAbility>> SearchBaseAbilities(string search)
        {
            IQueryable<BaseAbility> query = _dbContext.BaseAbilities;

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                query = query.Where(a => a.Name.Contains(search) || a.Description.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<BaseAbility>> GetBaseAbilities()
        {
            return await _dbContext.BaseAbilities.ToArrayAsync();
        }

        public async Task<BaseAbility> GetBaseAbility(int baseAbilityId)
        {
            return await _dbContext.BaseAbilities
                .FirstOrDefaultAsync(a => a.Id == baseAbilityId);
        }

        public async Task<BaseAbility> AddBaseAbility(BaseAbility baseAbility)
        {
            var result = await _dbContext.BaseAbilities.AddAsync(baseAbility);
            foreach (var value in baseAbility.ModifiesBaseTrainingValues)
            {
                _dbContext.Entry(value).State = EntityState.Unchanged;
            }
            _dbContext.Entry(baseAbility.ModifiesBaseTrainingValues).State = EntityState.Unchanged;
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<BaseAbility> UpdateBaseAbility(BaseAbility baseAbility)
        {
            var result = await _dbContext.BaseAbilities
                .FirstOrDefaultAsync(a => a.Id == baseAbility.Id);

            if (result != null)
            {
                result.Name = baseAbility.Name;
                result.Description = baseAbility.Description;
                result.HtmlDescription = baseAbility.HtmlDescription;
                result.AdvancesSkills = baseAbility.AdvancesSkills;
                result.IsProfessional = baseAbility.IsProfessional;
                result.ModifiesBaseTrainingValues = baseAbility.ModifiesBaseTrainingValues;
                result.SupportsBaseSkills = baseAbility.SupportsBaseSkills;
                result.UsesBaseAttributes = baseAbility.UsesBaseAttributes;
                result.Type = baseAbility.Type;

                //foreach (var value in baseAbility.ModifiesBaseTrainingValues)
                //{
                //    _dbContext.Entry(value).State = EntityState.Unchanged;
                //}
                _dbContext.Entry(baseAbility.ModifiesBaseTrainingValues).State = EntityState.Unchanged;

                return result;
            }

            return null;
        }

        public async Task<BaseAbility> DeleteBaseAbility(int baseAbilityId)
        {
            var result = await _dbContext.BaseAbilities
                .FirstOrDefaultAsync(a => a.Id == baseAbilityId);

            if (result != null)
            {
                _dbContext.BaseAbilities.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }

}

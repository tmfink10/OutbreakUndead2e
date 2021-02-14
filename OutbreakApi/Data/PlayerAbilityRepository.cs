using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public class PlayerAbilityRepository : IPlayerAbilityRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerAbilityRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PlayerAbility>> SearchPlayerAbilities(string search)
        {
            IQueryable<PlayerAbility> query = _dbContext.PlayerAbilities;

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                query = query.Where(a => a.BaseAbility.Name.Contains(search) || a.BaseAbility.Description.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<PlayerAbility>> GetPlayerAbilities()
        {
            return await _dbContext.PlayerAbilities.ToArrayAsync();
        }

        public async Task<PlayerAbility> GetPlayerAbility(int playerAbilityId)
        {
            return await _dbContext.PlayerAbilities
                .FirstOrDefaultAsync(a => a.Id == playerAbilityId);
        }

        public async Task<PlayerAbility> AddPlayerAbility(PlayerAbility playerAbility)
        {
            var result = await _dbContext.PlayerAbilities.AddAsync(playerAbility);
            foreach (var value in playerAbility.BaseAbility.ModifiesBaseTrainingValues)
            {
                _dbContext.Entry(value).State = EntityState.Unchanged;
            }
            foreach (var skill in playerAbility.SupportsPlayerSkills)
            {
                _dbContext.Entry(skill).State = EntityState.Unchanged;
                _dbContext.Entry(skill.BaseSkill).State = EntityState.Unchanged;
            }
            _dbContext.Entry(playerAbility.BaseAbility).State = EntityState.Unchanged;
            foreach (var attribute in playerAbility.BaseAbility.UsesBaseAttributes)
            {
                _dbContext.Entry(attribute).State = EntityState.Unchanged;
            }
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PlayerAbility> UpdatePlayerAbility(PlayerAbility playerAbility)
        {
            var result = await _dbContext.PlayerAbilities
                .FirstOrDefaultAsync(a => a.Id == playerAbility.Id);

            if (result != null)
            {
                result.BaseAbility = playerAbility.BaseAbility;
                result.Notes = playerAbility.Notes;
                result.Tier = playerAbility.Tier;
                result.SupportsPlayerSkills = playerAbility.SupportsPlayerSkills;
                result.Type = playerAbility.Type;
                result.AddedUsingBaseAttributeCode = playerAbility.AddedUsingBaseAttributeCode;


                foreach (var skill in playerAbility.SupportsPlayerSkills)
                {
                    _dbContext.Entry(skill.BaseSkill).State = EntityState.Unchanged;
                }
                foreach (var attribute in playerAbility.BaseAbility.UsesBaseAttributes)
                {
                    _dbContext.Entry(attribute).State = EntityState.Unchanged;
                }
                foreach (var value in playerAbility.BaseAbility.ModifiesBaseTrainingValues)
                {
                    _dbContext.Entry(value).State = EntityState.Unchanged;
                }
                _dbContext.Entry(playerAbility.BaseAbility).State = EntityState.Unchanged;
                _dbContext.PlayerAbilities.Update(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<PlayerAbility> DeletePlayerAbility(int playerAbilityId)
        {
            var result = await _dbContext.PlayerAbilities
                .FirstOrDefaultAsync(a => a.Id == playerAbilityId);

            if (result != null)
            {
                _dbContext.PlayerAbilities.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }

}

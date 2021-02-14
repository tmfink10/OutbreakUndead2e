using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public class PlayerCharacterRepository : IPlayerCharacterRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerCharacterRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PlayerCharacter>> SearchPlayerCharacters(string search)
        {
            IQueryable<PlayerCharacter> query = _dbContext.PlayerCharacters;

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                query = query.Where(p => p.FirstName.Contains(search) || p.LastName.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<PlayerCharacter>> GetPlayerCharacters()
        {
            return await _dbContext.PlayerCharacters.ToArrayAsync();
        }

        public async Task<PlayerCharacter> GetPlayerCharacter(int playerCharacterId)
        {
            return await _dbContext.PlayerCharacters
                .FirstOrDefaultAsync(p => p.Id == playerCharacterId);
        }

        public async Task<PlayerCharacter> AddPlayerCharacter(PlayerCharacter playerCharacter)
        {
            var result = await _dbContext.PlayerCharacters.AddAsync(playerCharacter);
            foreach (var attribute in playerCharacter.PlayerAttributes)
            {
                _dbContext.Entry(attribute.BaseAttribute).State = EntityState.Unchanged;
            }

            foreach (var ability in playerCharacter.PlayerAbilities)
            {
                _dbContext.Entry(ability.BaseAbility).State = EntityState.Unchanged;
                foreach (var skill in ability.SupportsPlayerSkills)
                {
                    _dbContext.Entry(skill.BaseSkill).State = EntityState.Unchanged;
                }
            }

            foreach (var skill in playerCharacter.PlayerSkills)
            {
                _dbContext.Entry(skill.BaseSkill).State = EntityState.Unchanged;
            }

            foreach (var value in playerCharacter.TrainingValues)
            {
                _dbContext.Entry(value.BaseTrainingValue).State = EntityState.Unchanged;
            }

            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PlayerCharacter> UpdatePlayerCharacter(PlayerCharacter playerCharacter)
        {
            var result = await _dbContext.PlayerCharacters
                .FirstOrDefaultAsync(p => p.Id == playerCharacter.Id);

            if (result != null)
            {
                result.FirstName = playerCharacter.FirstName;
                result.LastName = playerCharacter.LastName;
                result.Age = playerCharacter.Age;
                result.Sex = playerCharacter.Sex;
                result.SurvivalPoints = playerCharacter.SurvivalPoints;
                result.GestaltLevel = playerCharacter.GestaltLevel;
                result.CargoCapacity = playerCharacter.CargoCapacity;
                result.CompetencePoints = playerCharacter.CompetencePoints;
                result.HealthPoints = playerCharacter.HealthPoints;
                result.DamageThreshold = playerCharacter.DamageThreshold;
                result.Morale = playerCharacter.Morale;
                result.Notes = playerCharacter.Notes;

                _dbContext.PlayerCharacters.Update(result);
                foreach (var attribute in result.PlayerAttributes)
                {
                    _dbContext.Entry(attribute.BaseAttribute).State = EntityState.Unchanged;
                }

                foreach (var ability in result.PlayerAbilities)
                {
                    _dbContext.Entry(ability.BaseAbility).State = EntityState.Unchanged;
                    foreach (var skill in ability.SupportsPlayerSkills)
                    {
                        _dbContext.Entry(skill.BaseSkill).State = EntityState.Unchanged;
                    }
                }

                foreach (var skill in result.PlayerSkills)
                {
                    _dbContext.Entry(skill.BaseSkill).State = EntityState.Unchanged;
                }

                foreach (var value in playerCharacter.TrainingValues)
                {
                    _dbContext.Entry(value.BaseTrainingValue).State = EntityState.Unchanged;
                }

                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<PlayerCharacter> DeletePlayerCharacter(int playerCharacterId)
        {
            var result = await _dbContext.PlayerCharacters
                .FirstOrDefaultAsync(p => p.Id == playerCharacterId);

            if (result != null)
            {
                _dbContext.PlayerCharacters.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }

}

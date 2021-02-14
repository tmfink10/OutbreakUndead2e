using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public class PlayerSkillRepository : IPlayerSkillRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerSkillRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PlayerSkill>> SearchPlayerSkills(string search)
        {
            IQueryable<PlayerSkill> query = _dbContext.PlayerSkills;

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                query = query.Where(a => a.BaseSkill.Name.Contains(search) || a.BaseSkill.Description.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<PlayerSkill>> GetPlayerSkills()
        {
            return await _dbContext.PlayerSkills.ToArrayAsync();
        }

        public async Task<PlayerSkill> GetPlayerSkill(int playerSkillId)
        {
            return await _dbContext.PlayerSkills
                .FirstOrDefaultAsync(a => a.Id == playerSkillId);
        }

        public async Task<PlayerSkill> AddPlayerSkill(PlayerSkill playerSkill)
        {
            var result = await _dbContext.PlayerSkills.AddAsync(playerSkill);
            _dbContext.Entry(playerSkill.BaseSkill).State = EntityState.Unchanged;
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PlayerSkill> UpdatePlayerSkill(PlayerSkill playerSkill)
        {
            var result = await _dbContext.PlayerSkills
                .FirstOrDefaultAsync(a => a.Id == playerSkill.Id);

            if (result != null)
            {
                result.BaseSkill = playerSkill.BaseSkill;
                result.Notes = playerSkill.Notes;
                result.Value = playerSkill.Value;
                result.Advancements = playerSkill.Advancements;
                result.Type = playerSkill.Type;

                _dbContext.Entry(playerSkill.BaseSkill).State = EntityState.Unchanged;
                _dbContext.PlayerSkills.Update(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<PlayerSkill> DeletePlayerSkill(int playerSkillId)
        {
            var result = await _dbContext.PlayerSkills
                .FirstOrDefaultAsync(a => a.Id == playerSkillId);

            if (result != null)
            {
                _dbContext.PlayerSkills.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }

}

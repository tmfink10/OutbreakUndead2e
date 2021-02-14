using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public class PlayerAttributeRepository : IPlayerAttributeRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerAttributeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PlayerAttribute>> SearchPlayerAttributes(string search)
        {
            IQueryable<PlayerAttribute> query = _dbContext.PlayerAttributes;

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                query = query.Where(a => a.BaseAttribute.Name.Contains(search) || a.BaseAttribute.Description.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<PlayerAttribute>> GetPlayerAttributes()
        {
            return await _dbContext.PlayerAttributes.ToArrayAsync();
        }

        public async Task<PlayerAttribute> GetPlayerAttribute(int playerAttributeId)
        {
            return await _dbContext.PlayerAttributes
                .FirstOrDefaultAsync(a => a.Id == playerAttributeId);
        }

        public async Task<PlayerAttribute> AddPlayerAttribute(PlayerAttribute playerAttribute)
        {
            var result = await _dbContext.PlayerAttributes.AddAsync(playerAttribute);
            _dbContext.Entry(playerAttribute.BaseAttribute).State = EntityState.Unchanged;
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PlayerAttribute> UpdatePlayerAttribute(PlayerAttribute playerAttribute)
        {
            var result = await _dbContext.PlayerAttributes
                .FirstOrDefaultAsync(a => a.Id == playerAttribute.Id);

            if (result != null)
            {
                result.BaseAttribute = playerAttribute.BaseAttribute;
                result.Notes = playerAttribute.Notes;
                result.Value = playerAttribute.Value;
                result.Points = playerAttribute.Points;

                _dbContext.Entry(playerAttribute.BaseAttribute).State = EntityState.Unchanged;
                _dbContext.PlayerAttributes.Update(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<PlayerAttribute> DeletePlayerAttribute(int playerAttributeId)
        {
            var result = await _dbContext.PlayerAttributes
                .FirstOrDefaultAsync(a => a.Id == playerAttributeId);

            if (result != null)
            {
                _dbContext.PlayerAttributes.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public class BaseAttributeRepository : IBaseAttributeRepository

    {
        private readonly AppDbContext _dbContext;

        public BaseAttributeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BaseAttribute>> SearchBaseAttributes(string search)
        {
            IQueryable<BaseAttribute> query = _dbContext.BaseAttributes;

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                query = query.Where(a => a.Name.Contains(search) || a.Description.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<BaseAttribute>> GetBaseAttributes()
        {
            return await _dbContext.BaseAttributes.ToArrayAsync();
        }

        public async Task<BaseAttribute> GetBaseAttribute(int baseAttributeId)
        {
            return await _dbContext.BaseAttributes
                .FirstOrDefaultAsync(a => a.Id == baseAttributeId);
        }

        public async Task<BaseAttribute> AddBaseAttribute(BaseAttribute baseAttribute)
        {
            var result = await _dbContext.BaseAttributes.AddAsync(baseAttribute);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<BaseAttribute> UpdateBaseAttribute(BaseAttribute baseAttribute)
        {
            var result = await _dbContext.BaseAttributes
                .FirstOrDefaultAsync(a => a.Id == baseAttribute.Id);

            if (result != null)
            {
                result.Name = baseAttribute.Name;
                result.Description = baseAttribute.Description;
                result.HtmlDescription = baseAttribute.HtmlDescription;

                return result;
            }

            return null;
        }

        public async Task<BaseAttribute> DeleteBaseAttribute(int baseAttributeId)
        {
            var result = await _dbContext.BaseAttributes
                .FirstOrDefaultAsync(a => a.Id == baseAttributeId);

            if (result != null)
            {
                _dbContext.BaseAttributes.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }

}

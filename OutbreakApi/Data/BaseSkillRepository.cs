using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public class BaseSkillRepository : IBaseSkillRepository
    {
        private readonly AppDbContext _dbContext;

        public BaseSkillRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BaseSkill>> SearchBaseSkills(string search)
        {
            IQueryable<BaseSkill> query = _dbContext.BaseSkills;

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                query = query.Where(s => s.Name.Contains(search) || s.Description.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<BaseSkill>> GetBaseSkills()
        {
            return await _dbContext.BaseSkills.ToArrayAsync();
        }

        public async Task<BaseSkill> GetBaseSkill(int baseSkillId)
        {
            return await _dbContext.BaseSkills
                .FirstOrDefaultAsync(s => s.Id == baseSkillId);
        }

        public async Task<BaseSkill> AddBaseSkill(BaseSkill baseSkill)
        {
            var result = await _dbContext.BaseSkills.AddAsync(baseSkill);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<BaseSkill> UpdateBaseSkill(BaseSkill baseSkill)
        {
            var result = await _dbContext.BaseSkills
                .FirstOrDefaultAsync(s => s.Id == baseSkill.Id);

            if (result != null)
            {
                result.Name = baseSkill.Name;
                result.Description = baseSkill.Description;
                result.HtmlDescription = baseSkill.HtmlDescription;
                result.Type = baseSkill.Type;
                result.PrimaryAttributeBaseAttributeId = baseSkill.PrimaryAttributeBaseAttributeId;
                result.PrimaryAttribute = baseSkill.PrimaryAttribute;
                result.SecondaryAttributeBaseAttributeId = baseSkill.SecondaryAttributeBaseAttributeId;
                result.SecondaryAttribute = baseSkill.SecondaryAttribute;

                return result;
            }

            return null;
        }

        public async Task<BaseSkill> DeleteBaseSkill(int baseSkillId)
        {
            var result = await _dbContext.BaseSkills
                .FirstOrDefaultAsync(s => s.Id == baseSkillId);

            if (result != null)
            {
                _dbContext.BaseSkills.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }

}

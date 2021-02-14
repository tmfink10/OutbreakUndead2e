using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public interface IBaseSkillRepository
    {
        Task<IEnumerable<BaseSkill>> SearchBaseSkills(string search);
        Task<IEnumerable<BaseSkill>> GetBaseSkills();
        Task<BaseSkill> GetBaseSkill(int baseSkillId);
        Task<BaseSkill> AddBaseSkill(BaseSkill baseSkill);
        Task<BaseSkill> UpdateBaseSkill(BaseSkill baseSkill);
        Task<BaseSkill> DeleteBaseSkill(int baseSkillId);
    }
}

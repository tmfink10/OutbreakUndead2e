using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OutbreakApi.FluentConfigs;
using OutbreakModels.Models;

namespace OutbreakApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BaseAbility> BaseAbilities { get; set; }
        public DbSet<BaseAttribute> BaseAttributes { get; set; }
        public DbSet<BaseSkill> BaseSkills { get; set; }
        public DbSet<BaseTrainingValue> BaseTrainingValues { get; set; }
        public DbSet<PlayerAbility> PlayerAbilities { get; set; }
        public DbSet<PlayerAttribute> PlayerAttributes { get; set; }
        public DbSet<PlayerSkill> PlayerSkills { get; set; }
        public DbSet<PlayerTrainingValue> PlayerTrainingValues { get; set; }
        public DbSet<PlayerCharacter> PlayerCharacters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BaseAbilityConfig());
            modelBuilder.ApplyConfiguration(new BaseAttributeConfig());
            modelBuilder.ApplyConfiguration(new BaseSkillConfig());
            modelBuilder.ApplyConfiguration(new BaseTrainingValueConfig());
            modelBuilder.ApplyConfiguration(new PlayerAbilityConfig());
            modelBuilder.ApplyConfiguration(new PlayerAttributeConfig());
            modelBuilder.ApplyConfiguration(new PlayerCharacterConfig());
            modelBuilder.ApplyConfiguration(new PlayerSkillConfig());
            modelBuilder.ApplyConfiguration(new PlayerTrainingValueConfig());
        }
    }

}

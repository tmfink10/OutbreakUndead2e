using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutbreakModels.Models;

namespace OutbreakApi.FluentConfigs
{
    public class PlayerCharacterConfig : IEntityTypeConfiguration<PlayerCharacter>
    {
        public void Configure(EntityTypeBuilder<PlayerCharacter> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .HasMaxLength(60);
            builder.Property(c => c.LastName)
                .HasMaxLength(60);
            builder.Property(c => c.Sex)
                .HasMaxLength(20);

            //Notes intentionally omitted to be nvarchar(max)

            builder.HasMany<PlayerSkill>(c => c.PlayerSkills)
                .WithOne(s => s.PlayerCharacter);
            builder.HasMany<PlayerAbility>(c => c.PlayerAbilities)
                .WithOne(a => a.PlayerCharacter);
            builder.HasMany<PlayerTrainingValue>(c => c.TrainingValues)
                .WithOne(t => t.PlayerCharacter);
            builder.HasMany<PlayerAttribute>(c => c.PlayerAttributes)
                .WithOne(a => a.PlayerCharacter)
                .HasForeignKey(a => a.PlayerCharacterId);

            builder.HasData(new PlayerCharacter
            {
                Id = 1,
                FirstName = "Trevor",
                LastName = "Fink",
                Age = 35
            });
        }
    }

}

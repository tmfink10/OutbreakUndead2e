using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutbreakModels.Models;

namespace OutbreakApi.FluentConfigs
{
    public class PlayerAbilityConfig : IEntityTypeConfiguration<PlayerAbility>
    {
        public void Configure(EntityTypeBuilder<PlayerAbility> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Type)
                .HasMaxLength(50);
            builder.Property(a => a.Notes)
                .HasMaxLength(500);

            builder.HasOne<BaseAbility>(a => a.BaseAbility);
        }
    }

}

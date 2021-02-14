using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutbreakModels.Models;

namespace OutbreakApi.FluentConfigs
{
    public class PlayerAttributeConfig : IEntityTypeConfiguration<PlayerAttribute>
    {
        public void Configure(EntityTypeBuilder<PlayerAttribute> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Notes)
                .HasMaxLength(255);
            builder.Ignore(a => a.AdvancementValues);

            builder.HasOne<BaseAttribute>(a => a.BaseAttribute);

        }
    }

}

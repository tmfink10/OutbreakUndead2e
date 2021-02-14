using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutbreakModels.Models;

namespace OutbreakApi.FluentConfigs
{
    public class PlayerTrainingValueConfig : IEntityTypeConfiguration<PlayerTrainingValue>
    {
        public void Configure(EntityTypeBuilder<PlayerTrainingValue> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Type)
                .HasMaxLength(50);
            builder.Property(t => t.Notes)
                .HasMaxLength(500);

            builder.HasOne<BaseTrainingValue>(t => t.BaseTrainingValue);
        }
    }

}

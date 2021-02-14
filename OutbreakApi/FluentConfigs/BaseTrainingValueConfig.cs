using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutbreakModels.Models;

namespace OutbreakApi.FluentConfigs
{
    public class BaseTrainingValueConfig : IEntityTypeConfiguration<BaseTrainingValue>
    {
        public void Configure(EntityTypeBuilder<BaseTrainingValue> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(2500);
            builder.Property(t => t.HtmlDescription)
                .IsRequired()
                .HasMaxLength(2500);

            builder.HasData(new BaseTrainingValue
            {
                Id = 1,
                Name = "Archery Gear",
                Description = "Adds to Character's ability to use Archery Gear. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Archery Gear. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 2,
                Name = "Long Gun",
                Description = "Adds to Character's ability to use Long Guns. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Long Guns. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 3,
                Name = "Pistol",
                Description = "Adds to Character's ability to use Pistols. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Pistols See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 4,
                Name = "Bludgeon",
                Description = "Adds to Character's ability to use Bludgeoning Weapons. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Bludgeoning Weapons. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 5,
                Name = "Piercing",
                Description = "Adds to Character's ability to use Piercing Weapons. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Piercing Weapons. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 6,
                Name = "Slashing",
                Description = "Adds to Character's ability to use Slashing Weapons. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Slashing Weapons. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 7,
                Name = "Throwing",
                Description = "Adds to Character's ability to use Throwing Weapons. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Throwing Weapons. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 8,
                Name = "Martial Arts",
                Description = "Adds to Character's ability to use Martial Arts Weaponry. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Martial Arts Weaponry. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 9,
                Name = "Athletic Gear",
                Description = "Adds to Character's ability to use Athletic Gear. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Athletic Gear. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 10,
                Name = "Climbing Gear",
                Description = "Adds to Character's ability to use Climbing Gear. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Climbing Gear. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 11,
                Name = "Command Apparatus",
                Description = "Adds to Character's ability to use Command Equipment. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Command Equipment. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 12,
                Name = "Firefighting",
                Description = "Adds to Character's ability to use Firefighting Equipment. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Firefighting Equipment. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 13,
                Name = "First Aid Kit",
                Description = "Adds to Character's ability to use First Aid Kits. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use First Aid Kits. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 14,
                Name = "Medical Gear",
                Description = "Adds to Character's ability to use Medical Gear. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Medical Gear. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 15,
                Name = "Reconnaissance Gear",
                Description = "Adds to Character's ability to use Reconnaissance Gear. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Reconnaissance Gear. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 16,
                Name = "Survival Kit",
                Description = "Adds to Character's ability to use Survival Kits. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Survival Kits. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 17,
                Name = "Swimming/Diving",
                Description = "Adds to Character's ability to use Swimming and/or Diving Gear. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Swimming and/or Diving Gear. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 18,
                Name = "Tools",
                Description = "Adds to Character's ability to use Tools. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Tools. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 19,
                Name = "Value",
                Description = "Adds to Character's ability to use Currency. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Currency. See pp.122-3 "
            });

            builder.HasData(new BaseTrainingValue
            {
                Id = 20,
                Name = "Vehicles",
                Description = "Adds to Character's ability to use Vehicles. See pp.122-3 ",
                HtmlDescription = "Adds to Character's ability to use Vehicles. See pp.122-3 "
            });
        }
    }

}

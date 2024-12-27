using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Persistence.Configrations
{
    internal class ColorConfigration : IEntityTypeConfiguration<Domain.Entities.Color>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Color> builder)
        {
            builder.Property(x => x.Name).HasColumnType("varchar(100)");
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}

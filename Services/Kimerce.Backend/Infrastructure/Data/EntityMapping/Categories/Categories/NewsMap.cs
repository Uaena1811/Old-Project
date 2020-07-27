using Kimerce.Backend.Domain.News;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kimerce.Backend.Infrastructure.Data.EntityMapping.Locations
{
    public class NewsMap : IEntityMappingConfiguration<News>
    {
        public void Map(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News");
        }
    }
}

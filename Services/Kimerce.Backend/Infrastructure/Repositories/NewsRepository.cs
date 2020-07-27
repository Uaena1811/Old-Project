using Kimerce.Backend.Domain.News;
using Kimerce.Backend.Dto.Items.News;
using Kimerce.Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kimerce.Backend.Infrastructure.Repositories
{
    public class NewsRepository : Repository<News>
    {
        public NewsRepository (KimDbContext context) : base(context)
        {

        }
    }
}

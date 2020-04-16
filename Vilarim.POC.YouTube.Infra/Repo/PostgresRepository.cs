using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vilarim.POC.YouTube.Domain.Entities;
using Vilarim.POC.YouTube.Infra.Contracts.Repo;

namespace Vilarim.POC.YouTube.Infra.Repo
{
    public class PostgresRepository: IRepository
    {
         private YouTubeContext _context;

        public PostgresRepository(IServiceProvider service)
        {
            //_context = service.GetService<YouTubeContext>();
        }

        public async Task SaveRangeAsync(IList<ResponseSearchItem> entites)
        {
            //await _context.ResponseSearchItem.AddRangeAsync(entites);
            //await _context.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vilarim.POC.YouTube.Domain.Entities;

namespace Vilarim.POC.YouTube.Infra.Contracts.Repo
{
    public interface IRepository
    {
        Task SaveRangeAsync(IList<ResponseSearchItem> entites);
    }
}

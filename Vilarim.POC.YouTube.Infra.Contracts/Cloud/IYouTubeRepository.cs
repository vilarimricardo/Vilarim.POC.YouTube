using System.Collections.Generic;
using System.Threading.Tasks;
using Vilarim.POC.YouTube.Domain.Entities;

namespace Vilarim.POC.YouTube.Infra.Contracts.Cloud
{
    public interface IYouTubeRepository
    {
        Task<IList<ResponseSearchItem>> Search(string text);
    }

}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vilarim.POC.YouTube.Domain.Actions;
using Vilarim.POC.YouTube.Domain.Entities;
using Vilarim.POC.YouTube.Infra.Contracts.Cloud;

namespace Vilarim.POC.YouTube.Infra.ActionsHandler
{
    public class SearchOnYouTubeApiHandler : BaseActionHandler<SearchOnYouTubeApi, IList<ResponseSearchItem>>
    {
        private readonly IYouTubeRepository _youTubeRepository;
        public SearchOnYouTubeApiHandler(IServiceProvider serviceProvider, IYouTubeRepository youTubeRepository) : base(serviceProvider)
        {
            _youTubeRepository = youTubeRepository;
        }

        public override async Task<IList<ResponseSearchItem>> Handle(SearchOnYouTubeApi request, CancellationToken cancellationToken)
        {
            var result = await _youTubeRepository.Search(request.Search);

            //await _mediatr.Send(new PersistSearch(result));

            return result;
        }
    }
}
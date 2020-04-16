using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vilarim.POC.YouTube.Domain.Actions;
using Vilarim.POC.YouTube.Domain.Entities;
using Vilarim.POC.YouTube.Infra.Contracts.Cloud;
using Vilarim.POC.YouTube.Infra.Contracts.Repo;

namespace Vilarim.POC.YouTube.Infra.ActionsHandler
{
    public class PersistSearchHandler : BaseActionHandler<PersistSearch, ActionStatus>
    {
        private readonly IRepository _repo;

        public PersistSearchHandler(IServiceProvider serviceProvider, IRepository repo) : base(serviceProvider)
        {
            _repo=repo;
        }

        public override async Task<ActionStatus> Handle(PersistSearch request, CancellationToken cancellationToken)
        {
            await _repo.SaveRangeAsync(request.ResponseSearchItem);
            return ActionStatus.OK;
        }
    }
}
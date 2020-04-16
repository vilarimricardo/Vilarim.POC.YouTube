using System.Collections.Generic;
using Vilarim.POC.YouTube.Domain.Entities;

namespace Vilarim.POC.YouTube.Domain.Actions
{
    public class PersistSearch: Action<ActionStatus>{
        public IList<ResponseSearchItem> ResponseSearchItem {get; set;}

        public PersistSearch(IList<ResponseSearchItem> responseSearchItem)
        {
            ResponseSearchItem=responseSearchItem;
        }
    }
}
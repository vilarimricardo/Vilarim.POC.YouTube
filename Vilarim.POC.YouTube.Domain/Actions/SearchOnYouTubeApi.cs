using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vilarim.POC.YouTube.Domain.Entities;

namespace Vilarim.POC.YouTube.Domain.Actions
{
    public class SearchOnYouTubeApi: Action<IList<ResponseSearchItem>>{
        public string Search{get; set;}

        public SearchOnYouTubeApi(string text)
        {
            Search = text;
        }
    }
}
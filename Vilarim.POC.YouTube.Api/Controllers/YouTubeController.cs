using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vilarim.POC.YouTube.Domain.Actions;
using Vilarim.POC.YouTube.Domain.Entities;

namespace Vilarim.POC.YouTube.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YouTubeController : YouTubeControllerBase
    {
        public YouTubeController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        [Route("{searchText}")]
        public async Task<ActionResult<IList<ResponseSearchItem>>> Get(string searchText)
        {
            return await ActionResult(new SearchOnYouTubeApi(searchText));
        }
    }
}

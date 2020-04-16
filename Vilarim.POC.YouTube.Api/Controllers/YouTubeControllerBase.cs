using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Vilarim.POC.YouTube.Api.Controllers
{
    public class YouTubeControllerBase : Controller
    {
        protected readonly IMediator _mediatr;

        public YouTubeControllerBase(IServiceProvider serviceProvider)
        {
            _mediatr = serviceProvider.GetRequiredService<IMediator>();
        }

        protected async Task<ActionResult<R>> ActionResult<R>(Domain.Actions.Action<R> action)
        {
            try
            {
                var retorno = await _mediatr.Send<R>(action);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                })
                { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using Vilarim.POC.YouTube.Domain.Entities;
using Xunit;
using MediatR;
using Vilarim.POC.YouTube.Domain.Actions;

namespace Vilarim.POC.YouTube.Test
{
    public class YouTubeTest : TestBase
    {
        [Fact]
        public void TestController()
        {
            var client = CreateTestServerClient();

            var response = TaskHelper.RunSync(() => client.GetAsync("/api/YouTube/altered carbon"));

            var json = TaskHelper.RunSync(() => response.Content.ReadAsStringAsync());

            var result = json.JsonDeserialize<IList<ResponseSearchItem>>();

            result.Should().HaveCount(10);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public void TestCQRS()
        {

            using (var scope = CreateScope())
            {
                var mediatr = scope.ServiceProvider.GetService<IMediator>();

                var result = TaskHelper.RunSync(() => mediatr.Send(new SearchOnYouTubeApi("Rammstein")));

                result.Should().HaveCount(10);
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using Vilarim.POC.YouTube.Domain.Entities;
using Xunit;

namespace Vilarim.POC.YouTube.Test
{
    public class YouTubeTest: BaseTest
    {
        [Fact]
        public void TesteList(){
            var client = CreateTestServerClient();

            var response = TaskHelper.RunSync(() => client.GetAsync("/api/YouTube/altered carbon"));

            var json = TaskHelper.RunSync(() => response.Content.ReadAsStringAsync());

            var result = json.JsonDeserialize<IList<ResponseSearchItem>>();

            result.Should().HaveCount(8);
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

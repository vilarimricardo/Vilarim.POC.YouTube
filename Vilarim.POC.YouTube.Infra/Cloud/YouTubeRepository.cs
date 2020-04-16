
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Vilarim.POC.YouTube.Domain.Entities;
using Vilarim.POC.YouTube.Infra.Contracts.Cloud;

namespace Vilarim.POC.YouTube.Infra.Cloud
{
    public class YouTubeRepository : IYouTubeRepository
    {
        private static Google.Apis.Auth.OAuth2.UserCredential Autenticar()
        {
            Google.Apis.Auth.OAuth2.UserCredential credenciais;

            using (var stream = new System.IO.FileStream("client_secret.json", System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                var diretorioAtual = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var diretorioCredenciais = System.IO.Path.Combine(diretorioAtual, "credential");

                credenciais = Google.Apis.Auth.OAuth2.GoogleWebAuthorizationBroker.AuthorizeAsync(
                    Google.Apis.Auth.OAuth2.GoogleClientSecrets.Load(stream).Secrets,
                    new[] { Google.Apis.YouTube.v3.YouTubeService.Scope.YoutubeReadonly },
                    "user",
                    System.Threading.CancellationToken.None,
                    new Google.Apis.Util.Store.FileDataStore(diretorioCredenciais, true)).Result;
            }

            return credenciais;
        }

        public async Task<IList<ResponseSearchItem>> Search(string text)
        {
            var youtube = new Google.Apis.YouTube.v3.YouTubeService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = Autenticar()
            });

            SearchResource.ListRequest listRequest = youtube.Search.List("snippet");

            listRequest.Q = text;
            listRequest.MaxResults = 8;
            listRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;

            SearchListResponse searchResponse = await listRequest.ExecuteAsync();

            return searchResponse.Items.Select(item =>
                new ResponseSearchItem
                {
                    Name = item.Snippet.Title,
                    Url = item.Snippet.Thumbnails.Default__.Url,
                    Type = item.Id.Kind,
                    VideoId = item.Id.VideoId
                }
            ).ToList();
        }
    }
}
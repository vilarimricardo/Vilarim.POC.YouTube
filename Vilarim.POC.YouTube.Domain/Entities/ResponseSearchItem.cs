using System;

namespace Vilarim.POC.YouTube.Domain.Entities
{
    public class ResponseSearchItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string VideoId { get; set; }
    }
}
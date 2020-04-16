using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Vilarim.POC.YouTube.Domain.Entities;

namespace Vilarim.POC.YouTube.Infra
{
    public class ResponseSearchItemMap : IEntityTypeConfiguration<ResponseSearchItem>
    {
        private bool _inMemoryDbContext { get; set; }
        public ResponseSearchItemMap(bool inMemoryDbContext) => _inMemoryDbContext = inMemoryDbContext;

        public void Configure(EntityTypeBuilder<ResponseSearchItem> builder)
        {
            builder.HasKey(t => t.Id);

            if (_inMemoryDbContext)
                builder.Property(t => t.Id).HasValueGenerator<ConfigAppValueGenerator>();

           builder.ToTable("SEARCH_RESULT");
           builder.Property(t => t.Id).HasColumnName("CODIGO").ValueGeneratedOnAdd();
           builder.Property(t => t.Name).HasColumnName("TITULO").ValueGeneratedOnAdd();
           builder.Property(t => t.Url).HasColumnName("URL-TUMB").ValueGeneratedOnAdd();
           builder.Property(t => t.VideoId).HasColumnName("VIDEO_ID").ValueGeneratedOnAdd();
        }
    }

    public class ConfigAppValueGenerator : ValueGenerator
    {
        public override bool GeneratesTemporaryValues => false;

        protected override object NextValue(EntityEntry entry)
        {
            return Guid.NewGuid();
        }
    }
}
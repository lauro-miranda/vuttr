using LM.VUTTR.Api.Domain.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LM.VUTTR.Api.Infra.Mapping
{
    public class TagMapping : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(nameof(Tag));

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            builder.HasOne(x => x.Tool).WithMany(x => x.Tags).HasForeignKey(x => x.ToolId);
        }
    }
}
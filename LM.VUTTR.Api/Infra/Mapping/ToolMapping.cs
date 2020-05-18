using LM.VUTTR.Api.Domain.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LM.VUTTR.Api.Infra.Mapping
{
    public class ToolMapping : IEntityTypeConfiguration<Tool>
    {
        public void Configure(EntityTypeBuilder<Tool> builder)
        {
            builder.ToTable(nameof(Tool));

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Link).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(400).IsRequired();

            builder.HasOne(x => x.RegistrationUser).WithOne().HasForeignKey<Tool>(x => x.RegistrationUserId);
            builder.HasOne(x => x.UpdateUser).WithOne().HasForeignKey<Tool>(x => x.UpdateUserId);
        }
    }
}
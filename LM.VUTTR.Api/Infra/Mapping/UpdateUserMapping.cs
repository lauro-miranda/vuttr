using LM.VUTTR.Api.Domain.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LM.VUTTR.Api.Infra.Mapping
{
    public class UpdateUserMapping : IEntityTypeConfiguration<UpdateUser>
    {
        public void Configure(EntityTypeBuilder<UpdateUser> builder)
        {
            builder.ToTable(nameof(UpdateUser));

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}
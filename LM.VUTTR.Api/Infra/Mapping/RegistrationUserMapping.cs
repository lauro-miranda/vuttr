using LM.VUTTR.Api.Domain.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LM.VUTTR.Api.Infra.Mapping
{
    public class RegistrationUserMapping : IEntityTypeConfiguration<RegistrationUser>
    {
        public void Configure(EntityTypeBuilder<RegistrationUser> builder)
        {
            builder.ToTable(nameof(RegistrationUser));

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}
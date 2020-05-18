using LM.Domain.Valuables;
using LM.VUTTR.Api.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LM.VUTTR.Api.Infra.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.OwnsOne(p => p.Email, email =>
            {
                email.Property(p => p.Address).HasColumnName(nameof(Email)).HasMaxLength(256).IsRequired();
            });
        }
    }
}
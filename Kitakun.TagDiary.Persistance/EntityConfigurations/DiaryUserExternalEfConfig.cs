namespace Kitakun.TagDiary.Persistance.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Kitakun.TagDiary.Core.Domain.Users;

    public class DiaryUserExternalEfConfig : IEntityTypeConfiguration<DiaryUserExternalDataEntity>
    {
        public void Configure(EntityTypeBuilder<DiaryUserExternalDataEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ExternaluserId).IsRequired(true).HasMaxLength(255);
            builder.Property(x => x.ProviderName).IsRequired(true).HasMaxLength(255);

            builder.HasOne(x => x.User).WithMany(x => x.ExternalLogins).HasForeignKey(x => x.UserOwnerId);
        }
    }
}


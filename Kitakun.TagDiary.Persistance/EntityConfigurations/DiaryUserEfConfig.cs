namespace Kitakun.TagDiary.Persistance.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Kitakun.TagDiary.Core.Domain.Users;

    public class DiaryUserEfConfig : IEntityTypeConfiguration<DiaryUser>
    {
        public void Configure(EntityTypeBuilder<DiaryUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RegisteredAt).IsRequired(true);
        }
    }
}
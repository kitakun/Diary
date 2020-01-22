namespace Kitakun.TagDiary.Persistance.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Kitakun.TagDiary.Core.Domain;

    public class SpaceOwnerEfConfig : IEntityTypeConfiguration<SpaceOwner>
    {
        public void Configure(EntityTypeBuilder<SpaceOwner> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BlogPrivacy).IsRequired(true);

            builder.Property(x => x.LastRecordDoneAt).IsRequired(true);
            builder.Property(x => x.CreatedAt).IsRequired(true);

            builder.Property(x => x.MasterPasswordHash);

            builder.HasOne(x => x.UserOwner).WithOne().HasForeignKey<SpaceOwner>(x => x.UserOwnerId);

            builder.Property(x => x.HumanName).IsRequired().HasColumnType("varchar(255)");
            builder.Property(x => x.UrlName).IsRequired().HasColumnType("varchar(255)");
        }
    }
}

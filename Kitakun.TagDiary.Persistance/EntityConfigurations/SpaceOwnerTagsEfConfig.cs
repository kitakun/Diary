namespace Kitakun.TagDiary.Persistance.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Kitakun.TagDiary.Core.Domain;

    public class SpaceOwnerTagsEfConfig : IEntityTypeConfiguration<SpaceOwnerTags>
    {
        public void Configure(EntityTypeBuilder<SpaceOwnerTags> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AllTagsInSpace)
                .HasColumnType("varchar(255)[]");

            builder.HasOne(x => x.SpaceOwner)
                .WithOne(x => x.SpaceOwnerTagsEntity)
                .HasForeignKey<SpaceOwnerTags>(x => x.SpaceId);
        }
    }
}

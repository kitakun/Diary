namespace Kitakun.TagDiary.Persistance.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Kitakun.TagDiary.Core.Domain;

    public class DiaryRecordEfConfig : IEntityTypeConfiguration<DiaryRecord>
    {
        public void Configure(EntityTypeBuilder<DiaryRecord> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .IsRequired(true);

            builder.Property(x => x.MarkdownText)
                .IsRequired(true);

            builder.Property(x => x.Privacy);
            builder.Property(x => x.PasswordHash);
            builder.Property(x => x.ProtectedByPassword);
            builder.Property(x => x.ShortDescription)

                .IsRequired(true);
            builder.Property(x => x.TokenUrl)
                .IsRequired(true)
                .HasMaxLength(255);

            builder.Property(x => x.Tags)
                .HasColumnType("varchar(255)[]");

            builder.HasOne(x => x.SpaceOwner)
                .WithMany(x => x.Records)
                .HasForeignKey(x => x.SpaceId);
        }
    }
}

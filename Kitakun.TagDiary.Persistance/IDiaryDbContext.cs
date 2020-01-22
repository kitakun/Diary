namespace Kitakun.TagDiary.Persistance
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Kitakun.TagDiary.Core.Domain;
    using Kitakun.TagDiary.Core.Domain.Users;

    public interface IDiaryDbContext
    {
        DbSet<SpaceOwner> SpaceOwners { get; }

        DbSet<DiaryRecord> DiaryRecords { get; }

        DbSet<SpaceOwnerTags> SpaceOwnerTags { get; }

        DbSet<DiaryUser> DiaryUsers { get; }

        DbSet<DiaryUserExternalDataEntity> DiaryUserExternalDataEntities { get; }

        Task SaveChangesAsync();
    }
}

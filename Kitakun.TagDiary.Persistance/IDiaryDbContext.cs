namespace Kitakun.TagDiary.Persistance
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Kitakun.TagDiary.Core.Domain;

    public interface IDiaryDbContext
    {
        DbSet<SpaceOwner> SpaceOwners { get; }

        DbSet<DiaryRecord> DiaryRecords { get; }

        DbSet<SpaceOwnerTags> SpaceOwnerTags { get; }

        Task SaveChangesAsync();
    }
}

namespace Kitakun.TagDiary.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.Persistance;
    using Kitakun.TagDiary.Core.Domain;

    public class SpaceOwnerService : ISpaceOwnerService
    {
        private readonly IDiaryDbContext _dbContext;

        public SpaceOwnerService(IDiaryDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<bool> HasSpaceByUrlAsync(string url) =>
            _dbContext
                .SpaceOwners
                .AsNoTracking()
                .AnyAsync(a => a.UrlName == url.ToLower());

        public Task<int> GetSpaceOwnerIdByUrlAsync(string url) =>
            _dbContext
                .SpaceOwners
                .AsNoTracking()
                .Where(w => w.UrlName == url)
                .Select(s => s.Id)
                .FirstAsync();

        public Task CreateNewSpaceOwnerAsync(SpaceOwner entity)
        {
            entity.LastRecordDoneAt = DateTime.MinValue;
            entity.CreatedAt = DateTime.UtcNow;

            _dbContext.SpaceOwners.Add(entity);

            _dbContext.SpaceOwnerTags.Add(new SpaceOwnerTags
            {
                SpaceOwner = entity,
                AllTagsInSpace = new string[0]
            });

            return _dbContext.SaveChangesAsync();
        }

        public Task<SpaceOwner[]> NewestBlogsAsync() =>
            _dbContext
                .SpaceOwners
                .AsNoTracking()
                .Where(w => w.BlogPrivacy == PrivacyProtectionType.VisibleByAll)
                .OrderByDescending(x => x.LastRecordDoneAt)
                .Take(25)
                .ToArrayAsync();
    }
}

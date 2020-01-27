namespace Kitakun.TagDiary.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.Persistance;
    using Kitakun.TagDiary.Core.Domain;

    public class SpaceOwnerService : ISpaceOwnerService
    {
        private readonly IDiaryDbContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public SpaceOwnerService(
            IDiaryDbContext dbContext,
            IMemoryCache memoryCache)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public Task<bool> HasSpaceByUrlAsync(string url) =>
            _memoryCache.GetOrCreateAsync($"HasSpaceByUrlAsync({url})", (e) =>
                _dbContext
                    .SpaceOwners
                    .AsNoTracking()
                    .AnyAsync(a => a.UrlName == url.ToLower()));

        public Task<int> GetSpaceOwnerIdByUrlAsync(string url) =>
            _memoryCache.GetOrCreateAsync($"GetSpaceOwnerIdByUrlAsync({url})", (e) =>
                _dbContext
                    .SpaceOwners
                    .AsNoTracking()
                    .Where(w => w.UrlName == url)
                    .Select(s => s.Id)
                    .FirstOrDefaultAsync());

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
            _memoryCache.GetOrCreateAsync($"NewestBlogsAsync()", (e) =>
            {
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(25);
                return _dbContext
                    .SpaceOwners
                    .AsNoTracking()
                    .Where(w => w.BlogPrivacy == PrivacyProtectionType.VisibleByAll)
                    .OrderByDescending(x => x.LastRecordDoneAt)
                    .Take(25)
                    .ToArrayAsync();
            });

        public Task<string> GetMasterPasswordByUrlAsync(string url) =>
            _memoryCache.GetOrCreateAsync($"GetMasterPasswordByUrlAsync({url})", (e) =>
                _dbContext
                    .SpaceOwners
                    .AsNoTracking()
                    .Where(w => w.UrlName == url)
                    .Select(s => s.MasterPasswordHash)
                    .FirstOrDefaultAsync());

        public Task<SpaceOwner> LoadSpaceOfCurrentUserAsync(int userId) =>
            _dbContext
                .SpaceOwners
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.UserOwnerId == userId);
    }
}

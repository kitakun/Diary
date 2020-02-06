namespace Kitakun.TagDiary.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.Persistance;

    public class TagsService : ITagsService
    {
        private readonly IDiaryDbContext _dbContext;

        public TagsService(IDiaryDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public string[] LoadAllowedTagsByOwnerUrl(string currentSpaceUrlPrefix) =>
            _dbContext
                .SpaceOwnerTags
                .AsNoTracking()
                    .Where(w => w.SpaceOwner.UrlName == currentSpaceUrlPrefix)
                    .Select(s => s.AllTagsInSpace)
                    .FirstOrDefault();

        public Task<string[]> LoadAllTagsAsync(int spaceId) =>
            _dbContext
                .SpaceOwnerTags
                .AsNoTracking()
                .Where(w => w.SpaceId == spaceId)
                .Select(s => s.AllTagsInSpace)
                .FirstAsync();

        public string[] ParseTags(string tagInput) =>
            string.IsNullOrEmpty(tagInput)
                ? default
                : tagInput
                    .ToLower()
                    .Split("|");

        public async Task UpdateAllTags(int spaceId, string[] nonSavedTags = null)
        {
            // load entity
            var spaceOwnerEntity = await _dbContext
                .SpaceOwnerTags
                .SingleAsync(f => f.SpaceId == spaceId);
            // load all unique tags
            var loadedUniqueTags = await _dbContext
                .DiaryRecords
                .Where(w => w.SpaceId == spaceId
                    && w.Tags != null
                    // Теги из записей с паролем не сохраняем
                    && w.ProtectedByPassword == false)
                .SelectMany(sm => sm.Tags)
                .Distinct()
                .ToArrayAsync();
            // set tags to enttity
            spaceOwnerEntity.AllTagsInSpace = nonSavedTags != null
                ? nonSavedTags.Union(loadedUniqueTags).Distinct().ToArray()
                : loadedUniqueTags;
            // save
            await _dbContext.SaveChangesAsync();
        }
    }
}

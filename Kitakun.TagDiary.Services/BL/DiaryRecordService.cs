namespace Kitakun.TagDiary.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Kitakun.TagDiary.Core.Domain;
    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.Persistance;
    using Kitakun.TagDiary.Core.Domain.DiaryRecords;

    public class DiaryRecordService : IDiaryRecordService
    {
        private readonly IDiaryDbContext _dbContext;
        private readonly ITagsService _tagService;
        private readonly IEncrypter _ecnrypter;

        public DiaryRecordService(
            IDiaryDbContext dbContext,
            ITagsService tagService,
            IEncrypter ecnrypter)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
            _ecnrypter = ecnrypter ?? throw new ArgumentNullException(nameof(ecnrypter));
        }

        public Task CreateNewRecordAsync(DiaryRecord entity, string password = null)
        {
            _dbContext.DiaryRecords.Add(entity);

            entity.TokenUrl = GenerateToken();

            if (entity.ProtectedByPassword && !string.IsNullOrEmpty(password))
            {
                entity.ShortDescription = _ecnrypter.Encrypt(password, entity.ShortDescription);
                entity.MarkdownText = _ecnrypter.Encrypt(password, entity.MarkdownText);

                return _tagService.UpdateAllTags(entity.SpaceId, entity.Tags);
            }
            else if (entity.ProtectedByPassword)
            {
                throw new ArgumentException($"Включили защиту паролем, сам пароль не ввели");
            }

            return _dbContext.SaveChangesAsync();
        }

        private string GenerateToken(int length = 8, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
            if (string.IsNullOrEmpty(allowedChars))
                throw new ArgumentException("allowedChars may not be empty.");

            const int byteSize = 0x100;
            var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
            if (byteSize < allowedCharSet.Length)
                throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", byteSize));

            // Guid.NewGuid and System.Random are not particularly random. By using a
            // cryptographically-secure random number generator, the caller is always
            // protected, regardless of use.
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                var result = new StringBuilder();
                var buf = new byte[128];
                while (result.Length < length)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < length; ++i)
                    {
                        // Divide the byte into allowedCharSet-sized groups. If the
                        // random value falls into the last group and the last group is
                        // too small to choose from the entire allowedCharSet, ignore
                        // the value in order to avoid biasing the result.
                        var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
                        if (outOfRangeStart <= buf[i])
                            continue;
                        result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                    }
                }
                return result.ToString();
            }
        }

        public Task<DiaryRecord[]> LoadLastNRecordsAsync(
            int lastElementsCount,
            int spaceId,
            bool isOwner,
            DiaryRecordsFiltersTypeEnum filterType = DiaryRecordsFiltersTypeEnum.ShowAvailable,
            string[] tags = null,
            DateTime? dateFilter = null)
        {
            var query = _dbContext
                .DiaryRecords
                .AsNoTracking();

            if((int)filterType == 0)
            {
                filterType = DiaryRecordsFiltersTypeEnum.ShowAvailable;
            }

            if (isOwner)
            {
                switch (filterType)
                {
                    case DiaryRecordsFiltersTypeEnum.ShowAvailable:
                        query = query
                            .Where(w => w.SpaceId == spaceId
                                && w.Privacy == PrivacyProtectionType.VisibleByAll
                                && w.ProtectedByPassword == false);
                        break;
                    case DiaryRecordsFiltersTypeEnum.ShowAvailableWithPasswordProtection:
                        query = query
                            .Where(w => w.SpaceId == spaceId
                                && w.Privacy == PrivacyProtectionType.VisibleByAll);
                        break;
                    case DiaryRecordsFiltersTypeEnum.ShowProtectedWithPassword:
                        query = query
                            .Where(w => w.SpaceId == spaceId
                                && w.Privacy == PrivacyProtectionType.VisibleByAll
                                && w.ProtectedByPassword);
                        break;
                    default:
                        throw new NotImplementedException($"Type {filterType} not implemented for some reason");
                }
            }
            else
            {
                switch (filterType)
                {
                    case DiaryRecordsFiltersTypeEnum.ShowAvailable:
                        query = query
                            .Where(w => w.SpaceId == spaceId
                                && w.Privacy == PrivacyProtectionType.VisibleByAll
                                && w.ProtectedByPassword == false);
                        break;
                    case DiaryRecordsFiltersTypeEnum.ShowAvailableWithPasswordProtection:
                        query = query
                            .Where(w => w.SpaceId == spaceId
                                && w.Privacy == PrivacyProtectionType.VisibleByAll);
                        break;
                    case DiaryRecordsFiltersTypeEnum.ShowProtectedWithPassword:
                        query = query
                            .Where(w => w.SpaceId == spaceId
                                && w.Privacy == PrivacyProtectionType.VisibleByAll
                                && w.ProtectedByPassword);
                        break;
                    default:
                        throw new NotImplementedException($"Type {filterType} not implemented for some reason");
                }
            }

            if (tags != null && tags.Length > 0)
            {
                query = query
                    .Where(w => w.Tags != null && tags.All(a => w.Tags.Contains(a)));
            }

            if (dateFilter.HasValue)
            {
                query = query
                    .Where(w => w.CreatedAt.Year == dateFilter.Value.Year
                        && w.CreatedAt.Month == dateFilter.Value.Month
                        && w.CreatedAt.Day == dateFilter.Value.Day);
            }

            return query
                .OrderByDescending(x => x.CreatedAt)
                .Take(lastElementsCount)
                .ToArrayAsync();
        }

        public DiaryRecord LoadByToken(string recordKey) =>
            _dbContext
                .DiaryRecords
                .AsNoTracking()
                .Where(w => w.TokenUrl == recordKey)
                .FirstOrDefault();
    }
}

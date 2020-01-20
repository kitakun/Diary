namespace Kitakun.TagDiary.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Text;

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
            }
            else if (entity.ProtectedByPassword)
            {
                throw new ArgumentException($"Включили защиту паролем, сам пароль не ввели");
            }

            return _tagService.UpdateAllTags(entity.SpaceId, entity.Tags);
        }

        private string GenerateToken()
        {
            var urlsafe = new StringBuilder();
            var singleRandom = new Random();
            Enumerable.Range(48, 75)
              .Where(i => i < 58 || i > 64 && i < 91 || i > 96)
              .OrderBy(o => singleRandom.Next())
              .ToList()
              .ForEach(i => urlsafe.Append(Convert.ToChar(i)));
            var Token = urlsafe.ToString().Substring(singleRandom.Next(0, urlsafe.Length), singleRandom.Next(6, 8));

            return Token;
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

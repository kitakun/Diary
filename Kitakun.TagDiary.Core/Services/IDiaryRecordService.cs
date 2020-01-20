namespace Kitakun.TagDiary.Core.Services
{
    using System.Threading.Tasks;

    using Kitakun.TagDiary.Core.Domain;
    using Kitakun.TagDiary.Core.Domain.DiaryRecords;

    public interface IDiaryRecordService
    {
        Task CreateNewRecordAsync(DiaryRecord entity, string password = null);

        Task<DiaryRecord[]> LoadLastNRecordsAsync(
            int lastElementsCount,
            int spaceId,
            bool isOwner,
            DiaryRecordsFiltersTypeEnum filterType = DiaryRecordsFiltersTypeEnum.ShowAvailable,
            string[] tags = null);

        DiaryRecord LoadByToken(string recordKey);
    }
}

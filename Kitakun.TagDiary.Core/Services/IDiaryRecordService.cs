namespace Kitakun.TagDiary.Core.Services
{
    using System.Threading.Tasks;

    using Kitakun.TagDiary.Core.Domain;

    public interface IDiaryRecordService
    {
        Task CreateNewRecordAsync(DiaryRecord entity, string password = null);

        Task<DiaryRecord[]> LoadLastNRecordsAsync(int lastElementsCount, int spaceId, bool isOwner);

        DiaryRecord LoadByToken(string recordKey);
    }
}

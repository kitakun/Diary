namespace Kitakun.TagDiary.ViewModels.Models.SpaceOwnerModels
{
    using Kitakun.TagDiary.Core.Domain.DiaryRecords;

    public class SpaceOwnerIndexFilterModel
    {
        public DiaryRecordsFiltersTypeEnum PrivacyFilter { get; set; }

        public string TagInputString { get; set; }

        public string DateFilter { get; set; }
    }
}

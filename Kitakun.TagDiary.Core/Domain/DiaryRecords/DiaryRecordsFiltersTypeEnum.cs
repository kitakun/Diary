namespace Kitakun.TagDiary.Core.Domain.DiaryRecords
{
    using System.ComponentModel.DataAnnotations;

    public enum DiaryRecordsFiltersTypeEnum : byte
    {
        [Display(Name = "Видимые всем")]
        ShowAvailable = 1,

        [Display(Name = "Видимые и с паролями")]
        ShowAvailableWithPasswordProtection = 2,

        [Display(Name = "Только с паролями")]
        ShowProtectedWithPassword = 3,
    }
}

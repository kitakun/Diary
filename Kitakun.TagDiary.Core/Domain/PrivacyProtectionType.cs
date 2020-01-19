namespace Kitakun.TagDiary.Core.Domain
{
    using System.ComponentModel.DataAnnotations;

    public enum PrivacyProtectionType : byte
    {
        [Display(Name = "Виден всем")]
        VisibleByAll = 0,

        [Display(Name = "Виден по ссылке")]
        VisibleByLink = 1,

        /// <summary>
        /// Enter password once for all
        /// </summary>
        [Display(Name = "Виден после ввода пароля")]
        VisibleByMasterPassword = 2
    }
}

namespace Kitakun.TagDiary.ViewModels.Models.SpaceOwnerModels
{
    using System.ComponentModel.DataAnnotations;

    using Kitakun.TagDiary.Core.Domain;

    public class CreateNewSpaceModel
    {
        [Display(Name = "Web путь")]
        public string UrlName { get; set; }

        [Display(Name = "Настройки приватности")]
        public PrivacyProtectionType BlogPrivacy { get; set; }

        [Display(Name = "Пароль (если нужен)")]
        public string MasterPasswordHash { get; set; }
    }
}

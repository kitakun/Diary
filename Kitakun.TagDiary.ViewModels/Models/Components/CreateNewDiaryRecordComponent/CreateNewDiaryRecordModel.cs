namespace Kitakun.TagDiary.ViewModels.Models.Components.CreateNewDiaryRecordComponent
{
    using System.ComponentModel.DataAnnotations;

    using Kitakun.TagDiary.Core.Domain;

    public class CreateNewDiaryRecordModel
    {
        [Display(Name = "Краткое описание")]
        public string ShortDescription { get; set; }

        [Display(Name = "Внутренний текст")]
        public string MarkdownText { get; set; }

        [Display(Name = "Настройки приватности")]
        public PrivacyProtectionType Privacy { get; set; }

        [Display(Name = "Закодировать текст")]
        public bool ProtectedByPassword { get; set; }

        [Display(Name = "Пароль")]
        public string PasswordSource { get; set; }

        public string[] AllowedTags { get; set; }
        public string TagInput { get; set; }

        [Display(Name = "Теги")]
        public string[] Tags { get; set; }
    }
}

namespace Kitakun.TagDiary.Core.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DiaryRecord : IEntity
    {
        public int Id { get; set; }

        [MaxLength(255)]
        [Display(Name = "Record access by url (instead IDs)")]
        public string TokenUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ShortDescription { get; set; }

        public string MarkdownText { get; set; }

        public PrivacyProtectionType Privacy { get; set; }

        public bool ProtectedByPassword { get; set; }

        public int? PasswordHash { get; set; }

        public string[] Tags { get; set; }

        public int SpaceId { get; set; }

        public virtual SpaceOwner SpaceOwner { get; set; }
    }
}

namespace Kitakun.TagDiary.ViewModels.Models.SpaceOwnerModels
{
    using System;

    using Kitakun.TagDiary.Core.Domain;

    public class SpaceOwnerViewModel
    {
        /// <summary>
        /// SpaceOwner.Id
        /// </summary>
        public int SpaceId { get; set; }
        /// <summary>
        /// Current user is admin of space
        /// </summary>
        public bool IsAdmin { get; set; }

        public SpaceOwnerViewElementModel[] Records { get; set; }

        public string[] AllTags { get; set; }

        public SpaceOwnerIndexFilterModel Filter { get; set; }
    }

    public class SpaceOwnerViewElementModel
    {
        public string UrlToken { get; set; }

        public string ShortDescriptionText { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool HasPassword { get; set; }

        public string[] Tags { get; set; }

        public PrivacyProtectionType Privacy { get; set; }
    }
}

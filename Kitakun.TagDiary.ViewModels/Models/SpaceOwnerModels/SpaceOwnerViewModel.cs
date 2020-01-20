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

        /// <summary>
        /// Current loaded and filtered records
        /// </summary>
        public SpaceOwnerViewElementModel[] Records { get; set; }

        /// <summary>
        /// All tags for filter
        /// </summary>
        public string[] AllTags { get; set; }

        // Show on UI 'can't have records on {ResultsDate}
        public DateTime ResultsDate { get; set; }

        /// <summary>
        /// Filters model from UI
        /// </summary>
        public SpaceOwnerIndexFilterModel Filter { get; set; }
    }

    public class SpaceOwnerViewElementModel
    {
        /// <summary>
        /// Url token for access
        /// </summary>
        public string UrlToken { get; set; }

        /// <summary>
        /// Short record description
        /// </summary>
        public string ShortDescriptionText { get; set; }

        /// <summary>
        /// Record created at
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Record has password
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// Tags in record
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// Privacy setting
        /// </summary>
        public PrivacyProtectionType Privacy { get; set; }
    }
}

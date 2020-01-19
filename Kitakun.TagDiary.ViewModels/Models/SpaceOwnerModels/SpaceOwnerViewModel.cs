namespace Kitakun.TagDiary.ViewModels.Models.SpaceOwnerModels
{
    using System;

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
    }

    public class SpaceOwnerViewElementModel
    {
        public string UrlToken { get; set; }
        public string ShortDescriptionText { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool HasPassword { get; set; }
        public string[] Tags { get; set; }
    }
}

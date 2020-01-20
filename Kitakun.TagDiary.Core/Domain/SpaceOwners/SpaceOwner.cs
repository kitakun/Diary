namespace Kitakun.TagDiary.Core.Domain
{
    using System.Collections.Generic;

    public class SpaceOwner : IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Web url part http://www.{blog}.diary.com/
        /// </summary>
        public string UrlName { get; set; }

        /// <summary>
        /// Blog privacy type
        /// </summary>
        public PrivacyProtectionType BlogPrivacy { get; set; }

        /// <summary>
        /// Password for blog access (if we have protected by password privacy)
        /// </summary>
        public string MasterPasswordHash { get; set; }

        public virtual ICollection<DiaryRecord> Records { get; set; }

        public virtual SpaceOwnerTags SpaceOwnerTagsEntity { get; set; }
    }
}

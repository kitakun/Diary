namespace Kitakun.TagDiary.Core.Domain
{
    using System;
    using System.Collections.Generic;

    using Kitakun.TagDiary.Core.Domain.Users;

    public class SpaceOwner : IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Human readable name
        /// </summary>
        public string HumanName { get; set; }

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

        public DateTime LastRecordDoneAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserOwnerId { get; set; }

        public virtual DiaryUser UserOwner { get; set; }

        public virtual ICollection<DiaryRecord> Records { get; set; }

        public virtual SpaceOwnerTags SpaceOwnerTagsEntity { get; set; }
    }
}

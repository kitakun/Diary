namespace Kitakun.TagDiary.Core.Domain.Users
{
    using System;
    using System.Collections.Generic;

    public class DiaryUser : IEntity
    {
        public int Id { get; set; }

        public DateTime RegisteredAt { get; set; }

        public virtual ICollection<DiaryUserExternalDataEntity> ExternalLogins { get; set; }
    }
}

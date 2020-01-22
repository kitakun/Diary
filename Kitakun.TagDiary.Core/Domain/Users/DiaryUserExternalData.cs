namespace Kitakun.TagDiary.Core.Domain.Users
{
    public class DiaryUserExternalDataEntity : IEntity
    {
        public int Id { get; set; }

        public string ProviderName { get; set; }

        public string ExternaluserId { get; set; }

        public int UserOwnerId { get; set; }

        public virtual DiaryUser User { get; set; }
    }
}

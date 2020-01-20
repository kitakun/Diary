namespace Kitakun.TagDiary.Core.Domain
{
    public class SpaceOwnerTags : IEntity
    {
        public int Id { get; set; }

        public string[] AllTagsInSpace { get; set; }

        public int SpaceId { get; set; }

        public virtual SpaceOwner SpaceOwner { get; set; }
    }
}

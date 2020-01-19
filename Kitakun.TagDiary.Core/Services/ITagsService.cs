using System.Threading.Tasks;

namespace Kitakun.TagDiary.Core.Services
{
    public interface ITagsService
    {
        /// <summary>
        /// Load all existings tags from SpaceOwner Entity
        /// </summary>
        string[] LoadAllowedTagsByOwnerUrl(string currentSpaceUrlPrefix);

        /// <summary>
        /// parse raw single string with tags from client to separate tags array
        /// </summary>
        string[] ParseTags(string tagInput);

        /// <summary>
        /// Save unique tags in SpaceOwner entity
        /// </summary>
        Task UpdateAllTags(int spaceId, string[] nonSavedTags = null);
    }
}

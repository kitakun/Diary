namespace Kitakun.TagDiary.Core.Services
{
    using System.Threading.Tasks;

    using Kitakun.TagDiary.Core.Domain;

    public interface ISpaceOwnerService
    {
        Task<bool> HasSpaceByUrlAsync(string url);

        Task<int> GetSpaceOwnerIdByUrlAsync(string url);

        Task<string> GetMasterPasswordByUrlAsync(string url);

        /// <summary>
        /// Crud
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task CreateNewSpaceOwnerAsync(SpaceOwner entity);

        /// <summary>
        /// Load last N updated blogs for main page
        /// </summary>
        /// <returns></returns>
        Task<SpaceOwner[]> NewestBlogsAsync();

        /// <summary>
        /// Load SpaceOwner for current loggined user
        /// </summary>
        /// <returns></returns>
        Task<SpaceOwner> LoadSpaceOfCurrentUserAsync(int userId);
    }
}

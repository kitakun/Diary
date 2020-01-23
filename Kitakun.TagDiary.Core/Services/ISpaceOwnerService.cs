namespace Kitakun.TagDiary.Core.Services
{
    using System.Threading.Tasks;

    using Kitakun.TagDiary.Core.Domain;

    public interface ISpaceOwnerService
    {
        Task<bool> HasSpaceByUrlAsync(string url);

        Task<int> GetSpaceOwnerIdByUrlAsync(string url);

        Task<string> GetMasterPasswordByUrlAsync(string url);

        Task CreateNewSpaceOwnerAsync(SpaceOwner entity);

        Task<SpaceOwner[]> NewestBlogsAsync();
    }
}

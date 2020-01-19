namespace Kitakun.TagDiary.Core.Services
{
    public interface IWebContext
    {
        /// <summary>
        /// Current space Url part
        /// </summary>
        string CurrentSpaceUrlPrefix { get; }

        /// <summary>
        /// Current user is admin of current space
        /// </summary>
        bool IsSpaceOwner { get; }
    }
}

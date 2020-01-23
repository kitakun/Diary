namespace Kitakun.TagDiary.Web.Infrastructure
{
    using System;

    using Microsoft.AspNetCore.Http;

    using Kitakun.TagDiary.Core.Services;

    public class WebContext : IWebContext
    {
        private readonly Lazy<string> _spaceOwnerLazy;
        public string CurrentSpaceUrlPrefix => _spaceOwnerLazy.Value;

        private readonly Lazy<bool> _isSpaceOwner;
        public bool IsSpaceOwner => _isSpaceOwner.Value;

        public WebContext(
            IHttpContextAccessor webContextAccessor,
            ISpaceOwnerService spaceOwnerService)
        {
            _spaceOwnerLazy = new Lazy<string>(() =>
            {
                var host = webContextAccessor.HttpContext.Request.Host.Host;
                var dotSplit = host.Split(".");
#if DEBUG
                const int initialElementsCount = 2;
#else
                const int initialElementsCount = 3;
#endif
                if (dotSplit.Length >= initialElementsCount)
                {
                    return dotSplit[0].ToLower();
                }

                return string.Empty;
            });

            _isSpaceOwner = new Lazy<bool>(() =>
            {
                var rawUserId = webContextAccessor.HttpContext.User.Identity.Name;
                if (!string.IsNullOrEmpty(rawUserId) && int.TryParse(rawUserId, out var userId))
                {
                    var actualOwnerId = spaceOwnerService.GetSpaceOwnerIdByUrlAsync(_spaceOwnerLazy.Value).GetAwaiter().GetResult();
                    return actualOwnerId == userId;
                }

                return false;
            });
        }
    }
}

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

        public WebContext(IHttpContextAccessor webContextAccessor)
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

            _isSpaceOwner = new Lazy<bool>(() => true);
        }
    }
}

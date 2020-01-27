namespace Kitakun.TagDiary.Web.Infrastructure
{
    using System;

    using Microsoft.AspNetCore.Http;

    using Kitakun.TagDiary.Core.Services;
    using Microsoft.AspNetCore.Mvc.Infrastructure;

    public class WebContext : IWebContext
    {
        private readonly IActionContextAccessor _actionContext;

        private readonly Lazy<string> _spaceOwnerLazy;
        public string CurrentSpaceUrlPrefix => _spaceOwnerLazy.Value;

        private readonly Lazy<bool> _isSpaceOwner;
        public bool IsSpaceOwner => _isSpaceOwner.Value;

        public WebContext(
            IHttpContextAccessor webContextAccessor,
            IActionContextAccessor actionContext,
            ISpaceOwnerService spaceOwnerService)
        {
            _actionContext = actionContext ?? throw new ArgumentNullException(nameof(actionContext));

            _spaceOwnerLazy = new Lazy<string>(() =>
            {
                var ownerName = _actionContext
                    .ActionContext
                    .RouteData
                    .Values[DiaryWebConstants.RouteByOwnerName]?
                    .ToString();

                return string.IsNullOrEmpty(ownerName)
                    ? string.Empty
                    : ownerName;
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

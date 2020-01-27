namespace Kitakun.TagDiary.Web.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.Web.Extensions;

    public class DiaryUrlService
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IWebContext _webContext;

        public DiaryUrlService(
            IUrlHelper urlHelper,
            IWebContext webContext)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _webContext = webContext ?? throw new ArgumentNullException(nameof(webContext));
        }

        public IActionResult RedirectTo<TController>(string methodNameOf)
        {
            var url = _urlHelper.RouteUrl(
                "defaultWithOwner",
                new
                {
                    directSpaceOwnerUrl = _webContext.CurrentSpaceUrlPrefix,
                    controller = ControllerExtensions.GetControllerName<TController>(),
                    action = methodNameOf
                });

            return new RedirectResult(url);
        }

        public IActionResult RedirectTo<TController>(string methodNameOf, string urlName)
        {
            var url = _urlHelper.RouteUrl(
                "defaultWithOwner",
                new
                {
                    directSpaceOwnerUrl = urlName,
                    controller = ControllerExtensions.GetControllerName<TController>(),
                    action = methodNameOf
                });

            return new RedirectResult(url);
        }

        public string UrlToAction<TController>(string methodNameOf, dynamic rawQueryParams = null)
        {
            var routeValues = new Dictionary<string, string>();
            if (rawQueryParams != null)
            {
                var queryParams = new RouteValueDictionary(rawQueryParams);
                foreach (var queryParam in queryParams)
                {
                    routeValues[queryParam.Key] = queryParam.Value.ToString();
                }
            }

            routeValues["directSpaceOwnerUrl"] = _webContext.CurrentSpaceUrlPrefix;
            routeValues["controller"] = ControllerExtensions.GetControllerName<TController>();
            routeValues["action"] = methodNameOf;

            return _urlHelper.RouteUrl(
                "defaultWithOwner",
                values: routeValues);
        }
    }
}

namespace Kitakun.TagDiary.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;

    using Kitakun.ExternalLogin.Abstraction;
    using Kitakun.ExternalLogin.Vk;
    using Kitakun.TagDiary.Web.Extensions;
    using Kitakun.TagDiary.Web.Infrastructure;

    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthProvider[] _allProviders;
        private readonly IAuthService _authService;

        public AuthController(
            IAuthProvider[] allProviders,
            IAuthService authService)
        {
            _allProviders = allProviders ?? throw new ArgumentNullException(nameof(allProviders));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpGet]
        public async Task<IActionResult> ExternalAuth([FromRoute] string providerName)
        {
            providerName = providerName.ToLower();
            var authProvider = default(IAuthProvider);
            switch (providerName)
            {
                case "vk":
                    authProvider = _allProviders.First(f => f is VkAuth);
                    break;
                default:
                    throw new NotImplementedException($"provider {providerName} not implemented");
            }

            var info = authProvider.ParseCallback(HttpContext);

            await _authService.Auth(info, typeof(VkAuth).Name);

            return RedirectToAction(
                nameof(HomeController.Index),
                ControllerExtensions.GetControllerName<HomeController>());
        }

        [HttpGet]
        public IActionResult MoveToProvider([FromQuery] string providerName)
        {
            var authProvider = default(IAuthProvider);
            switch (providerName)
            {
                case "vk":
                    authProvider = _allProviders.First(f => f is VkAuth);
                    break;
                default:
                    throw new NotImplementedException($"provider {providerName} not implemented");
            }

            return authProvider.RedirectToProvider();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Response.Cookies.Delete(DiaryWebConstants.MasterPasswordCookieName);

            return RedirectToAction(
                nameof(HomeController.Index),
                ControllerExtensions.GetControllerName<HomeController>());
        }
    }
}

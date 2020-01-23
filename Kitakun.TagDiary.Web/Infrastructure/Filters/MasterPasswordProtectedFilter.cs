namespace Kitakun.TagDiary.Web.Infrastructure
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Configuration;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.Web.Controllers;
    using Kitakun.TagDiary.Web.Extensions;

    public class MasterPasswordProtectedFilter : ActionFilterAttribute
    {
        private readonly IWebContext _webContext;
        private readonly ISpaceOwnerService _spaceOwnerService;
        private readonly IConfiguration _appConfig;
        private readonly IEncrypter _encrypter;
        private readonly IMd5 _md5;

        public MasterPasswordProtectedFilter(
            IWebContext webContext,
            ISpaceOwnerService spaceOwnerService,
            IConfiguration appConfig,
            IEncrypter encrypter,
            IMd5 md5)
        {
            _webContext = webContext ?? throw new System.ArgumentNullException(nameof(webContext));
            _spaceOwnerService = spaceOwnerService ?? throw new System.ArgumentNullException(nameof(spaceOwnerService));
            _appConfig = appConfig ?? throw new System.ArgumentNullException(nameof(appConfig));
            _encrypter = encrypter ?? throw new System.ArgumentNullException(nameof(encrypter));
            _md5 = md5 ?? throw new System.ArgumentNullException(nameof(md5));
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!_webContext.IsSpaceOwner)
            {
                var passwordHash = await _spaceOwnerService.GetMasterPasswordByUrlAsync(_webContext.CurrentSpaceUrlPrefix);

                if (passwordHash != default)
                {
                    // get value from master pass cookie
                    var enteredPasswordHash = context
                        .HttpContext
                        .Request
                        .Cookies[DiaryWebConstants.MasterPasswordCookieName]
                        ?? string.Empty;

                    var enteredPassword = !string.IsNullOrEmpty(enteredPasswordHash)
                        ? _encrypter.Decrypt(_appConfig.GetSection("All").GetValue<string>("AppSecret"), enteredPasswordHash)
                        : string.Empty;
                    var enteredPasswordHashMd5 = !string.IsNullOrEmpty(enteredPasswordHash)
                        ? _md5.Hash(enteredPassword)
                        : string.Empty;

                    if (string.IsNullOrEmpty(enteredPasswordHashMd5) || passwordHash != enteredPasswordHashMd5)
                    {
                        if (!string.IsNullOrEmpty(enteredPassword))
                        {
                            // Remove master pass cookie
                            context.HttpContext.Response.Cookies.Delete(DiaryWebConstants.MasterPasswordCookieName);
                        }

                        context.Result = new RedirectToActionResult(
                            nameof(SpaceOwnerController.MasterPassword),
                            ControllerExtensions.GetControllerName<SpaceOwnerController>(),
                            null);
                        return;
                    }
                }
            }

            await next();
        }
    }
}

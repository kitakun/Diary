namespace Kitakun.TagDiary.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.Extensions.Caching.Memory;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.ViewModels.Models;
    using Kitakun.TagDiary.Web.Infrastructure.Services;

    public class HomeController : Controller
    {
        private readonly IWebContext _webContext;
        private readonly ISpaceOwnerService _spaceOwnerService;
        private readonly IMemoryCache _memCache;
        private readonly DiaryUrlService _diaryUrlHelper;

        public HomeController(
            IWebContext webContext,
            ISpaceOwnerService spaceOwnerService,
            IMemoryCache memCache,
            DiaryUrlService diaryUrlHelper)
        {
            _webContext = webContext ?? throw new ArgumentNullException(nameof(webContext));
            _spaceOwnerService = spaceOwnerService ?? throw new ArgumentNullException(nameof(spaceOwnerService));
            _memCache = memCache ?? throw new ArgumentNullException(nameof(memCache));
            _diaryUrlHelper = diaryUrlHelper ?? throw new ArgumentNullException(nameof(diaryUrlHelper));
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var spaceOwnerRedirection = _webContext.CurrentSpaceUrlPrefix;
            if (!string.IsNullOrEmpty(spaceOwnerRedirection))
            {
                if (await _spaceOwnerService.HasSpaceByUrlAsync(spaceOwnerRedirection))
                {
                    return _diaryUrlHelper.RedirectTo<SpaceOwnerController>(nameof(Index));
                }
                else
                {
                    return _diaryUrlHelper.RedirectTo<HomeController>(nameof(NotFoundSpaceOwner));
                }
            }
            else
            {
                var existingBlogs = await _memCache.GetOrCreateAsync(nameof(_spaceOwnerService.NewestBlogsAsync),
                    (e) =>
                    {
                        e.SetAbsoluteExpiration(new TimeSpan(0, 30, 0));
                        return _spaceOwnerService.NewestBlogsAsync();
                    });

                return View(existingBlogs);
            }
        }

        [HttpGet]
        public IActionResult NotFoundSpaceOwner() =>
            View(model: _webContext.CurrentSpaceUrlPrefix);

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return View(new ErrorViewModel {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorText = TempData?["error"]?.ToString() ?? context.Error.Message
            });
        }
    }
}

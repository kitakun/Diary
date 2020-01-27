namespace Kitakun.TagDiary.Web.Components
{
    using Microsoft.AspNetCore.Mvc;

    using Kitakun.TagDiary.Web.Infrastructure.Services;
    using Kitakun.TagDiary.Web.Controllers;
    using Kitakun.TagDiary.Core.Services;

    public class BackToHomeComponent : ViewComponent
    {
        private readonly DiaryUrlService _diaryUrlHelper;
        private readonly IWebContext _webContext;

        public BackToHomeComponent(
            DiaryUrlService diaryUrlHelper,
            IWebContext webContext)
        {
            _diaryUrlHelper = diaryUrlHelper ?? throw new System.ArgumentNullException(nameof(diaryUrlHelper));
            _webContext = webContext ?? throw new System.ArgumentNullException(nameof(webContext));
        }

        public IViewComponentResult Invoke()
        {
            var href = default(string);
            if (!string.IsNullOrEmpty(_webContext.CurrentSpaceUrlPrefix))
            {
                href = _diaryUrlHelper.UrlToAction<SpaceOwnerController>(nameof(SpaceOwnerController.Index));
            }

            return View(nameof(BackToHomeComponent), model: href);
        }
    }
}

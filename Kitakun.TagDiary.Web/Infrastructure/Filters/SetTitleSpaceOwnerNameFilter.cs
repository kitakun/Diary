namespace Kitakun.TagDiary.Web.Infrastructure.Filters
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

    using Kitakun.TagDiary.Core.Services;

    public class SetTitleSpaceOwnerNameFilter : ActionFilterAttribute
    {
        private readonly IWebContext _webContext;

        public SetTitleSpaceOwnerNameFilter(IWebContext webContext)
        {
            _webContext = webContext ?? throw new System.ArgumentNullException(nameof(webContext));
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ViewResult viewResult)
            {
                dynamic viewBag = new DynamicViewData(() => viewResult.ViewData);
                viewBag.Title = _webContext.CurrentSpaceUrlPrefix;
            }

            base.OnResultExecuting(context);
        }
    }
}

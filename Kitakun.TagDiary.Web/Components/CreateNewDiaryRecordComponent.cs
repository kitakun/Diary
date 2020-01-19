namespace Kitakun.TagDiary.Web.Components
{
    using Microsoft.AspNetCore.Mvc;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.ViewModels.Models.Components.CreateNewDiaryRecordComponent;

    public class CreateNewDiaryRecordComponent : ViewComponent
    {
        private readonly IWebContext _webContext;
        private readonly ITagsService _tagsService;

        public CreateNewDiaryRecordComponent(
            IWebContext webContext,
            ITagsService tagsService)
        {
            _webContext = webContext ?? throw new System.ArgumentNullException(nameof(webContext));
            _tagsService = tagsService ?? throw new System.ArgumentNullException(nameof(tagsService));
        }

        public IViewComponentResult Invoke() =>
            View(nameof(CreateNewDiaryRecordComponent), new CreateNewDiaryRecordModel
            {
                AllowedTags = _tagsService.LoadAllowedTagsByOwnerUrl(_webContext.CurrentSpaceUrlPrefix)
            });
    }
}

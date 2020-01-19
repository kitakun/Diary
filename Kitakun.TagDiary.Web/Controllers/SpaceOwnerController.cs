namespace Kitakun.TagDiary.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.Core.Domain;
    using Kitakun.TagDiary.Web.Extensions;
    using Kitakun.TagDiary.ViewModels.Models.SpaceOwnerModels;
    using Kitakun.TagDiary.ViewModels.Models.Components.CreateNewDiaryRecordComponent;
    using Kitakun.TagDiary.Core.Extensions;

    public class SpaceOwnerController : Controller
    {
        private readonly IWebContext _webContext;
        private readonly ISpaceOwnerService _spaceOwnerService;
        private readonly IDiaryRecordService _diaryRecordService;
        private readonly IMapperService _mapperService;
        private readonly ITagsService _tagsService;

        public SpaceOwnerController(
            IWebContext webContext,
            ISpaceOwnerService spaceOwnerService,
            IDiaryRecordService diaryRecordService,
            IMapperService mapperService,
            ITagsService tagsService)
        {
            _webContext = webContext ?? throw new ArgumentNullException(nameof(webContext));
            _spaceOwnerService = spaceOwnerService ?? throw new ArgumentNullException(nameof(spaceOwnerService));
            _diaryRecordService = diaryRecordService ?? throw new ArgumentNullException(nameof(diaryRecordService));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
            _tagsService = tagsService ?? throw new ArgumentNullException(nameof(tagsService));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var urlPart = _webContext.CurrentSpaceUrlPrefix;
            if (string.IsNullOrEmpty(urlPart))
            {
                return RedirectToAction(nameof(HomeController.Index), ControllerExtensions.GetControllerName<HomeController>());
            }

            var spaceId = await _spaceOwnerService.GetSpaceOwnerIdByUrlAsync(urlPart);

            const int loadLastElementsCount = 25;

            var lastRecords = await _diaryRecordService.LoadLastNRecordsAsync(loadLastElementsCount, spaceId, _webContext.IsSpaceOwner);

            return View(new SpaceOwnerViewModel
            {
                SpaceId = spaceId,
                IsAdmin = _webContext.IsSpaceOwner,
                Records = lastRecords.CastArray(_mapperService.Map<DiaryRecord, SpaceOwnerViewElementModel>)
            });
        }

        [HttpGet]
        public IActionResult CreateNew() => View();

        [HttpPost]
        public async Task<IActionResult> CreateNew([FromForm] CreateNewSpaceModel model)
        {
            var newEntity = new SpaceOwner
            {
                BlogPrivacy = model.BlogPrivacy,
                MasterPasswordHash = model.MasterPasswordHash,
                UrlName = model.UrlName.ToLower()
            };

            await _spaceOwnerService.CreateNewSpaceOwnerAsync(newEntity);

            var redirectUrl = Request.RedirectToSubdomain(newEntity.UrlName);

            return new RedirectResult(redirectUrl, false);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewDiaryRecord([FromForm] CreateNewDiaryRecordModel model)
        {
            var spaceId = await _spaceOwnerService.GetSpaceOwnerIdByUrlAsync(_webContext.CurrentSpaceUrlPrefix);

            var newEntity = new DiaryRecord
            {
                ShortDescription = model.ShortDescription,
                MarkdownText = model.MarkdownText,
                CreatedAt = DateTime.UtcNow,
                Privacy = model.Privacy,
                ProtectedByPassword = model.ProtectedByPassword,
                SpaceId = spaceId,
                Tags = _tagsService.ParseTags(model.TagInput),
                PasswordHash = model.ProtectedByPassword
                    ? model.PasswordSource.GetHashCode()
                    : default
            };

            await _diaryRecordService.CreateNewRecordAsync(newEntity, model.PasswordSource);

            return RedirectToAction(nameof(SpaceOwnerController.Index));
        }
    }
}

namespace Kitakun.TagDiary.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.Core.Domain;
    using Kitakun.TagDiary.Web.Extensions;
    using Kitakun.TagDiary.ViewModels.Models.SpaceOwnerModels;
    using Kitakun.TagDiary.ViewModels.Models.Components.CreateNewDiaryRecordComponent;
    using Kitakun.TagDiary.Core.Extensions;
    using Kitakun.TagDiary.Web.Infrastructure;

    public class SpaceOwnerController : Controller
    {
        private readonly IWebContext _webContext;
        private readonly ISpaceOwnerService _spaceOwnerService;
        private readonly IDiaryRecordService _diaryRecordService;
        private readonly IMapperService _mapperService;
        private readonly ITagsService _tagsService;
        private readonly IConfiguration _appConfig;
        private readonly IEncrypter _encrypter;
        private readonly IMd5 _md5;

        public SpaceOwnerController(
            IWebContext webContext,
            ISpaceOwnerService spaceOwnerService,
            IDiaryRecordService diaryRecordService,
            IMapperService mapperService,
            ITagsService tagsService,
            IConfiguration appConfig,
            IEncrypter encrypter,
            IMd5 md5)
        {
            _webContext = webContext ?? throw new ArgumentNullException(nameof(webContext));
            _spaceOwnerService = spaceOwnerService ?? throw new ArgumentNullException(nameof(spaceOwnerService));
            _diaryRecordService = diaryRecordService ?? throw new ArgumentNullException(nameof(diaryRecordService));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
            _tagsService = tagsService ?? throw new ArgumentNullException(nameof(tagsService));
            _appConfig = appConfig ?? throw new ArgumentNullException(nameof(appConfig));
            _encrypter = encrypter ?? throw new ArgumentNullException(nameof(encrypter));
            _md5 = md5 ?? throw new ArgumentNullException(nameof(md5));
        }

        [HttpGet]
        [ServiceFilter(typeof(MasterPasswordProtectedFilter))]
        public async Task<IActionResult> Index()
        {
            var urlPart = _webContext.CurrentSpaceUrlPrefix;
            if (string.IsNullOrEmpty(urlPart))
            {
                return RedirectToAction(nameof(HomeController.Index), ControllerExtensions.GetControllerName<HomeController>());
            }

            var spaceId = await _spaceOwnerService.GetSpaceOwnerIdByUrlAsync(urlPart);

            const int loadLastElementsCount = 25;

            var allTags = await _tagsService.LoadAllTagsAsync(spaceId);
            var lastRecords = await _diaryRecordService.LoadLastNRecordsAsync(loadLastElementsCount, spaceId, _webContext.IsSpaceOwner);

            return View(new SpaceOwnerViewModel
            {
                SpaceId = spaceId,
                IsAdmin = _webContext.IsSpaceOwner,
                Records = lastRecords.CastArray(_mapperService.Map<DiaryRecord, SpaceOwnerViewElementModel>),
                AllTags = allTags,
                ResultsDate = DateTime.Now
            });
        }

        [HttpPost]
        [ServiceFilter(typeof(MasterPasswordProtectedFilter))]
        public async Task<IActionResult> Index([FromForm]SpaceOwnerIndexFilterModel filter)
        {
            var urlPart = _webContext.CurrentSpaceUrlPrefix;
            if (string.IsNullOrEmpty(urlPart))
            {
                return RedirectToAction(nameof(HomeController.Index), ControllerExtensions.GetControllerName<HomeController>());
            }

            var spaceId = await _spaceOwnerService.GetSpaceOwnerIdByUrlAsync(urlPart);

            const int loadLastElementsCount = 25;

            var allTags = await _tagsService.LoadAllTagsAsync(spaceId);

            var parsedDateFilter = default(DateTime?);
            if (!string.IsNullOrEmpty(filter.DateFilter))
            {
                var splittedDay = filter.DateFilter.Split(".");
                parsedDateFilter = new DateTime(
                    int.Parse(splittedDay[2]),
                    int.Parse(splittedDay[1]) + 1,
                    int.Parse(splittedDay[0]));
            }
            var tagsFromFilter = _tagsService.ParseTags(filter.TagInputString);
            var lastRecords = await _diaryRecordService.LoadLastNRecordsAsync(
                loadLastElementsCount,
                spaceId,
                _webContext.IsSpaceOwner,
                filter.PrivacyFilter,
                tagsFromFilter,
                parsedDateFilter);

            return View(new SpaceOwnerViewModel
            {
                SpaceId = spaceId,
                IsAdmin = _webContext.IsSpaceOwner,
                Records = lastRecords.CastArray(_mapperService.Map<DiaryRecord, SpaceOwnerViewElementModel>),
                AllTags = allTags,
                ResultsDate = parsedDateFilter ?? DateTime.Now,
                Filter = filter
            });
        }

        [HttpGet]
        public IActionResult MasterPassword() => View();

        [HttpPost]
        public async Task<IActionResult> MasterPassword([FromForm]string passwrd)
        {
            var passwordHash = await _spaceOwnerService.GetMasterPasswordByUrlAsync(_webContext.CurrentSpaceUrlPrefix);
            var enteredPasswordHash = _md5.Hash(passwrd);
            if (passwordHash == enteredPasswordHash)
            {
                var encryptedPassword = _encrypter.Encrypt(_appConfig.GetSection("All").GetValue<string>("AppSecret"), passwrd);

                HttpContext.Response.Cookies.Append(
                    DiaryWebConstants.MasterPasswordCookieName,
                    encryptedPassword,
                    new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        IsEssential = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(10)
                    });

                return RedirectToAction(nameof(SpaceOwnerController.Index));
            }
            else
            {
                HttpContext.Response.Cookies.Delete(DiaryWebConstants.MasterPasswordCookieName);
            }

            return View(model: "Пароль не подходит");
        }

        [HttpGet]
        public IActionResult CreateNew() => View();

        [HttpPost]
        public async Task<IActionResult> CreateNew([FromForm] CreateNewSpaceModel model)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);

            var newEntity = new SpaceOwner
            {
                BlogPrivacy = model.BlogPrivacy,
                MasterPasswordHash = _md5.Hash(model.MasterPasswordString),
                UrlName = model.UrlName.ToLower(),
                HumanName = model.HumanName,
                UserOwnerId = userId
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

        [HttpGet]
        public IActionResult RedirectToSub([FromQuery] string key) =>
            new RedirectResult(Request.RedirectToSubdomain(key), false);
    }
}

namespace Kitakun.TagDiary.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.Web.Exceptions;

    public class DiaryRecordController : Controller
    {
        private readonly IDiaryRecordService _diaryService;
        private readonly IEncrypter _encrypter;

        public DiaryRecordController(
            IDiaryRecordService diaryService,
            IEncrypter encrypter)
        {
            _diaryService = diaryService ?? throw new System.ArgumentNullException(nameof(diaryService));
            _encrypter = encrypter ?? throw new System.ArgumentNullException(nameof(encrypter));
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string recordKey)
        {
            var entity = _diaryService.LoadByToken(recordKey);
            if(entity == null)
            {
                throw new NotFoundWebException("Запись по данной ссылке не сущесвует либо скрыта!");
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult Index([FromForm] string tokenUrl, [FromForm] string password)
        {
            var entity = _diaryService.LoadByToken(tokenUrl);
            if (entity == null)
            {
                throw new NotFoundWebException("Запись по данной ссылке не сущесвует либо скрыта!");
            }

            entity.ShortDescription = _encrypter.Decrypt(password, entity.ShortDescription);
            entity.MarkdownText = _encrypter.Decrypt(password, entity.MarkdownText);

            return View(entity);
        }
    }
}

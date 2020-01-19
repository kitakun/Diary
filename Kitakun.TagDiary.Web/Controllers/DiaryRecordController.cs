namespace Kitakun.TagDiary.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.Web.Exceptions;

    public class DiaryRecordController : Controller
    {
        private readonly IDiaryRecordService _diaryService;

        public DiaryRecordController(IDiaryRecordService diaryService)
        {
            _diaryService = diaryService ?? throw new System.ArgumentNullException(nameof(diaryService));
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
    }
}

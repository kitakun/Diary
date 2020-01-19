namespace Kitakun.TagDiary.Web.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetControllerName<TController>() =>
            typeof(TController).Name.Replace("Controller", string.Empty);
    }
}

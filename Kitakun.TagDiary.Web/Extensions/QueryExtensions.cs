namespace Kitakun.TagDiary.Web.Extensions
{
    using Microsoft.AspNetCore.Http;

    public static class QueryExtensions
    {
        public static string RedirectToSubdomain(this HttpRequest context, string subdomainName)
        {
#if DEBUG
            const int initialElementsCount = 2;
#else
            const int initialElementsCount = 3;
#endif
            //TODO remove existing subdomain or throw exception

            return $"{context.Scheme}://{subdomainName}.{context.Host}/";
        }
    }
}

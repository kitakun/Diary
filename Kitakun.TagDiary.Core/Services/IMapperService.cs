namespace Kitakun.TagDiary.Core.Services
{
    public interface IMapperService
    {
        T Map<F, T>(F src)
            where F : class
            where T : class;
    }
}

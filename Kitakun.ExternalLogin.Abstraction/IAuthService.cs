namespace Kitakun.ExternalLogin.Abstraction
{
    using System.Threading.Tasks;

    public interface IAuthService
    {
        Task Auth(AuthData data, string providerName);
    }
}

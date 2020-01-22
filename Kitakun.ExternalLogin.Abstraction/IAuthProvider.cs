namespace Kitakun.ExternalLogin.Abstraction
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public interface IAuthProvider
    {
        IActionResult RedirectToProvider();

        AuthData ParseCallback(HttpContext context);
    }
}

namespace Kitakun.TagDiary.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.EntityFrameworkCore;

    using Kitakun.ExternalLogin.Abstraction;
    using Kitakun.TagDiary.Persistance;
    using Kitakun.TagDiary.Core.Domain.Users;

    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDiaryDbContext _dbContext;

        public AuthService(
            IHttpContextAccessor accessor,
            IDiaryDbContext dbContext)
        {
            _contextAccessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Auth(AuthData data, string providerName)
        {
            var httpContext = _contextAccessor.HttpContext;

            var userId = await CreateUserIfNotExists(providerName, data.UserId);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

            var stringUserId = userId.ToString();
            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.Name, stringUserId),
                new Claim(ClaimTypes.NameIdentifier, stringUserId),
                new Claim(ClaimTypes.Role, "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await httpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme,
              new ClaimsPrincipal(claimsIdentity),
              authProperties);
        }

        private async Task<int> CreateUserIfNotExists(string providerNameRaw, string externalUserId)
        {
            var providerName = providerNameRaw.ToLower();

            var existingUserId = await _dbContext
                .DiaryUserExternalDataEntities
                .Where(a => a.ExternaluserId == externalUserId
                    && a.ProviderName == providerName)
                .Select(s => s.UserOwnerId)
                .FirstOrDefaultAsync();
            if (existingUserId > 0)
            {
                return existingUserId;
            }
            else
            {
                // register
                var newUser = new DiaryUser
                {
                    RegisteredAt = DateTime.UtcNow
                };
                var newExternalAuth = new DiaryUserExternalDataEntity
                {
                    ProviderName = providerName,
                    User = newUser,
                    ExternaluserId = externalUserId,
                };
                await _dbContext.DiaryUserExternalDataEntities.AddAsync(newExternalAuth);

                await _dbContext.SaveChangesAsync();

                return newUser.Id;
            }
        }
    }
}

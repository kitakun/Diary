namespace Kitakun.ExternalLogin.Vk
{
    using System;
    using System.Net.Http;
    using System.Text;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    using Kitakun.ExternalLogin.Abstraction;
    using Kitakun.ExternalLogin.Vk.Models;

    public class VkAuth : IAuthProvider
    {
        public const string ApiVersion = "5.8";

        private readonly int _appId;
        private readonly string _backUrl;
        private readonly string _appSecret;

        private readonly IConfiguration _appConfig;

        public VkAuth(IConfiguration appConfig)
        {
            _appConfig = appConfig ?? throw new ArgumentNullException(nameof(appConfig));

            _backUrl = $"{_appConfig.GetSection("All")["SiteUrl"]}externalAuth/Vk?";
            _appSecret = _appConfig.GetSection("ExternalLogins")["VkAppSecret"];
            _appId = int.Parse(_appConfig.GetSection("ExternalLogins")["VkAppId"]);
        }

        public IActionResult RedirectToProvider()
        {
            var url = $"https://oauth.vk.com/authorize?client_id={_appId}&display=page&redirect_uri={_backUrl}&response_type=code&v={ApiVersion}";

            return new RedirectResult(url, false);
        }

        public AuthData ParseCallback(HttpContext context)
        {
            if (!context.Request.Query.TryGetValue("code", out var atokenStrVal))
            {
                throw new Exception($"While accessing vk get response without access token");
            }

            using (var client = new HttpClient())
            {
                var accessTokenUrl = $"https://oauth.vk.com/access_token?client_id={_appId}&client_secret={_appSecret}&code={atokenStrVal.ToString()}&v={ApiVersion}&redirect_uri={_backUrl}";
                var tokenModel = default(VkAccessTokenModel);
                using (var resp = client.GetAsync(accessTokenUrl).Result)
                {
                    var tokenResult = resp.Content.ReadAsStringAsync().Result;
                    tokenModel = Newtonsoft.Json.JsonConvert.DeserializeObject<VkAccessTokenModel>(tokenResult);
                }

                var accInfoUrl = $"https://api.vk.com/method/users.get?v={ApiVersion}&access_token={tokenModel.AccessToken}";
                var requestTask = client.PostAsync(accInfoUrl, new StringContent(string.Empty, Encoding.UTF8, "application/json"));
                using (var resp = requestTask.Result)
                {
                    var result = resp.Content.ReadAsStringAsync().Result;
                    var vkUser = Newtonsoft.Json.JsonConvert.DeserializeObject<VkUserResponse>(result);

                    if (vkUser != null && vkUser.Response != null && vkUser.Response.Length > 0)
                    {
                        return new AuthData
                        {
                            Token = tokenModel.AccessToken,
                            UserId = vkUser.Response[0].UserId.ToString(),
                            UserName = $"{vkUser.Response[0].FirstName} {vkUser.Response[0].LastName}"
                        };
                    }
                }
            }

            throw new Exception($"Не получилось войти через вк :с");
        }
    }
}

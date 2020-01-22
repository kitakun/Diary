namespace Kitakun.ExternalLogin.Vk
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;

    using Kitakun.ExternalLogin.Abstraction;
    using Kitakun.ExternalLogin.Vk.Models;

    public class VkAuth : IAuthProvider
    {
        public const int AppId = 7290460;
        public const string BackUrl = "https://localhost:5011/externalAuth/Vk?";
        public const string AppSecret = "5z4PwEYmfnCkDrcLWJa5";
        public const string ApiVersion = "5.8";

        public IActionResult RedirectToProvider()
        {
            var url = $"https://oauth.vk.com/authorize?client_id={AppId}&display=page&redirect_uri={BackUrl}&response_type=code&v={ApiVersion}";

            return new RedirectResult(url, false);
        }

        public AuthData ParseCallback(HttpContext context)
        {
            if (!context.Request.Query.TryGetValue("code", out var atokenStrVal))
            {
                throw new Exception($"While accessing vk get response without access token");
            }

            using(var client = new HttpClient())
            {
                var accessTokenUrl = $"https://oauth.vk.com/access_token?client_id={AppId}&client_secret={AppSecret}&code={atokenStrVal.ToString()}&v={ApiVersion}&redirect_uri={BackUrl}";
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

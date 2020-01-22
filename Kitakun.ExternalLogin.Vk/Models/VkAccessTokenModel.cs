namespace Kitakun.ExternalLogin.Vk.Models
{
    using Newtonsoft.Json;

    public class VkAccessTokenModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public long Expires { get; set; }
    }
}

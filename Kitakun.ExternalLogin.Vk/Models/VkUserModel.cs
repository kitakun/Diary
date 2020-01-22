namespace Kitakun.ExternalLogin.Vk.Models
{
    using Newtonsoft.Json;

    public class VkUserResponse
    {
        [JsonProperty("response")]
        public VkUserModel[] Response { get; set; }
    }

    public class VkUserModel
    {
        [JsonProperty("id")]
        public long UserId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}

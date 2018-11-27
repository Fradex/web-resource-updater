using Newtonsoft.Json;

namespace WebPackUpdater.Model
{
    public class AuthRequestModel
    {
		[JsonProperty("username")]
	    public string UserName { get; set; }
	    [JsonProperty("password")]
		public string Password { get; set; }
    }
}

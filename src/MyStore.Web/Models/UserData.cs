using Newtonsoft.Json;

namespace MyStore.Web.Models
{
    public class UserData
    {
        public int Id { get; set; }
        [JsonProperty("first_name")] public string FirstName { get; set; }
        [JsonProperty("last_name")] public string LastName { get; set; }
    }
}
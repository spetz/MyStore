using System;

namespace MyStore.Infrastructure.Auth
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
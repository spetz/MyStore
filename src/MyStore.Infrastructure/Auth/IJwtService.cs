using System.Collections.Generic;

namespace MyStore.Infrastructure.Auth
{
    public interface IJwtService
    {
        JsonWebToken Create(string userId, string username, string role,
            IDictionary<string, string> claims);
    }
}
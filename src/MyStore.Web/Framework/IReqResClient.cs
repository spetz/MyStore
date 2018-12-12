using System.Collections.Generic;
using System.Threading.Tasks;
using MyStore.Web.Models;

namespace MyStore.Web.Framework
{
    public interface IReqResClient
    {
        Task<IEnumerable<UserData>> GetUsersAsync();
    }
}
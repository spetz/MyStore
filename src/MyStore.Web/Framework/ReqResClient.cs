using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MyStore.Web.Models;

namespace MyStore.Web.Framework
{
    public class ReqResClient : IReqResClient
    {
        private readonly Random _random = new Random();
        private readonly HttpClient _httpClient;

        public ReqResClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<IEnumerable<UserData>> GetUsersAsync()
        {
            var page = _random.Next(1, 5);
            var response = await _httpClient.GetAsync($"users?page={page}");
            if (!response.IsSuccessStatusCode)
            {
                return Enumerable.Empty<UserData>();
            }

            var data = await response.Content.ReadAsAsync<UsersResponse>();

            return data.Data;
        }

        private class UsersResponse
        {
            public IEnumerable<UserData> Data { get; set; }
        }
    }
}
using Newtonsoft.Json;
using Railtown.Interview.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Railtown.Interview.Api.Clients
{
    [ExcludeFromCodeCoverage]
    public class UsersApiClient : IUsersApiClient
    {
        private readonly HttpClient _httpClient;
        public UsersApiClient(HttpClient httpClient) =>
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        public async Task<List<User>> GetUsers()
        {
            var query = "/users";
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            var response = await _httpClient.GetAsync(query).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<List<User>>(responseData);
            }
            else
            {
                throw new HttpRequestException($"Taxes server returned status code {(int)response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}

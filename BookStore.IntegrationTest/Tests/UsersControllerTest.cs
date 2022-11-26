using BookStore.Business.Dto.RequestObjects;
using BookStore.IntegrationTest.ExpectedResults;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.IntegrationTest.Tests
{
    public class UsersControllerTest
    {
        private HttpClient _httpClient;
        private UsersControllerExpectedResult _result;
        private string _token = String.Empty;

        public UsersControllerTest()
        {
            var factory = new WebApplicationFactory<Program>();
            _httpClient = factory.CreateDefaultClient();
            _result = new UsersControllerExpectedResult();
            _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJUaGlzSXNKd3RTdWJqZWN0IiwianRpIjoiMTliZDhlMjUtZTdkNS00ZjA3LTgxMmItNzZhNjgxOTczN2JmIiwiVWlkIjoiMiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkN1c3RvbWVyIiwiZXhwIjoxNjcwMDU4NjIwLCJpc3MiOiJCb29rU3RvcmUiLCJhdWQiOiJCb29rU3RvcmUifQ.b9a4C6Zy2xad2RYxEZpaBrqpUm3UJ2nKDZkPI5kIEUA";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        [Fact]
        public async void UpdateProfile_ReturnNewProfile()
        {
            var newProfile = new UpdateProfileRequest { FullName = "Lâm Khánh Chi", Email = null, Gender = true, PhoneNumber = "0987654322" };
            var json = JsonConvert.SerializeObject(newProfile, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/v1/users", data);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetOwnProfile_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void UpdateProfile_WrongFormatPhoneNumber_ReturnBadRequest()
        {
            var newProfile = new UpdateProfileRequest { FullName = "Lâm Khánh Chi", Email = null, Gender = true, PhoneNumber = "9087654321" };
            var json = JsonConvert.SerializeObject(newProfile, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/v1/users", data);

            Assert.True(!response.IsSuccessStatusCode);
        }

        [Fact]
        public async void GetOwnProfile_ReturnOwnProfile()
        {
            var response = await _httpClient.GetAsync("api/v1/users");

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetOwnProfile_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }
    }
}

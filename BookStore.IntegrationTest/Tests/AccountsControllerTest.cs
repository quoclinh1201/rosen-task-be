using BookStore.Business.Dto.RequestObjects;
using BookStore.IntegrationTest.ExpectedResults;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.IntegrationTest.Tests
{
    public class AccountsControllerTest
    {
        private HttpClient _httpClient;
        private AccountsControllerExpectedResult _result;
        private string _token = String.Empty;

        public AccountsControllerTest()
        {
            var factory = new WebApplicationFactory<Program>();
            _httpClient = factory.CreateDefaultClient();
            _result = new AccountsControllerExpectedResult();
        }

        [Fact]
        public async void LoginWithUsernameAndPassword_TrueUsernameAndPassword_ReturnToken()
        {
            var request = new LoginRequest { Username = "customer1", Password = "123" };
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/v1/accounts/login", data);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void LoginWithUsernameAndPassword_WrongUsernameAndPassword_ReturnNotFound()
        {
            var request = new LoginRequest { Username = "wrong_username", Password = "123" };
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/v1/accounts/login", data);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.LoginWithUsernameAndPassword_WrongUsernameAndPassword_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(!response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void CreateAccount_HappyCase_ReturnToken()
        {
            var request = new CreateAccountRequest { Username = "customer4", Password = "123", ConfirmPassword = "123" };
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/v1/accounts/create-account", data);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void CreateAccount_DuplicateUsername_ReturnBadRequest()
        {
            var request = new CreateAccountRequest { Username = "customer1", Password = "123", ConfirmPassword = "123" };
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/v1/accounts/create-account", data);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.CreateAccount_DuplicateUsername_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(!response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void CreateAccount_ConfirmPasswordNotMatch_ReturnBadRequest()
        {
            var request = new CreateAccountRequest { Username = "customer5", Password = "123", ConfirmPassword = "1234" };
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/v1/accounts/create-account", data);

            Assert.True(!response.IsSuccessStatusCode);
        }
    }
}

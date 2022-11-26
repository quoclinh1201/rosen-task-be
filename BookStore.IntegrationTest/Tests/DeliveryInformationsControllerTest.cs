using BookStore.Business.Dto.RequestObjects;
using BookStore.IntegrationTest.ExpectedResults;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BookStore.IntegrationTest.Tests
{
    public class DeliveryInformationsControllerTest
    {
        private HttpClient _httpClient;
        private DeliveryInformationsControllerExpectedResult _result;
        private string _token = String.Empty;

        public DeliveryInformationsControllerTest()
        {
            var factory = new WebApplicationFactory<Program>();
            _httpClient = factory.CreateDefaultClient();
            _result = new DeliveryInformationsControllerExpectedResult();
            _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJUaGlzSXNKd3RTdWJqZWN0IiwianRpIjoiMTliZDhlMjUtZTdkNS00ZjA3LTgxMmItNzZhNjgxOTczN2JmIiwiVWlkIjoiMiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkN1c3RvbWVyIiwiZXhwIjoxNjcwMDU4NjIwLCJpc3MiOiJCb29rU3RvcmUiLCJhdWQiOiJCb29rU3RvcmUifQ.b9a4C6Zy2xad2RYxEZpaBrqpUm3UJ2nKDZkPI5kIEUA";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        [Fact]
        public async void CreateDeliveryInformation_ReturnListDeliveryInformations()
        {
            var request = new CreateDeliveryInformationRequest { ReceiverName = "Nguyễn Văn A", ReceiverPhoneNumber = "0987654123", DeliveryAddress = "177/4 Linh Trung, Phường Linh Trung, Thủ Đức, Thành phố Hồ Chí Minh" };
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/v1/delivery-information", data);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.CreateDeliveryInformation_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetAllDeliveryInformation_ReturnListDeliveryInformations()
        {
            var response = await _httpClient.GetAsync("api/v1/delivery-information");

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetAllDeliveryInformation_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void DeleteDeliveryInformation_Id_ReturnListDeliveryInformations()
        {
            var id = 5;
            var response = await _httpClient.DeleteAsync("api/v1/delivery-information/" + id);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.DeleteDeliveryInformation_Id_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }
    }
}

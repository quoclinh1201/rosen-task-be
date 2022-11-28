using BookStore.Business.Dto.RequestObjects;
using BookStore.Data.Entities;
using BookStore.IntegrationTest.ExpectedResults;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Text;

namespace BookStore.IntegrationTest.Tests
{
    public class OrdersControllerTest
    {
        private HttpClient _httpClient;
        private OrdersControllerExpectedResult _result;
        private string _token = String.Empty;

        public OrdersControllerTest()
        {
            var factory = new WebApplicationFactory<Program>();
            _httpClient = factory.CreateDefaultClient();
            _result = new OrdersControllerExpectedResult();
            _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJUaGlzSXNKd3RTdWJqZWN0IiwianRpIjoiMTliZDhlMjUtZTdkNS00ZjA3LTgxMmItNzZhNjgxOTczN2JmIiwiVWlkIjoiMiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkN1c3RvbWVyIiwiZXhwIjoxNjcwMDU4NjIwLCJpc3MiOiJCb29rU3RvcmUiLCJhdWQiOiJCb29rU3RvcmUifQ.b9a4C6Zy2xad2RYxEZpaBrqpUm3UJ2nKDZkPI5kIEUA";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        [Fact]
        public async void GetAllOrders_ReturnOrders()
        {
            var response = await _httpClient.GetAsync("api/v1/orders");

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetAllOrders_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void ReOrder_OrderId_ReturnTrue()
        {
            var orderId = 4;
            var response = await _httpClient.PostAsync("api/v1/orders/" + orderId, null);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.ReOrder_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void CheckOut_ReturnOrderId()
        {
            var request = new CreateOrderRequest { DeliveryInformation = 2, PaymentMethod = "COD"};
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/v1/orders", data);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.CheckOut_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetOrderDetail_OrderId_ReturnOrderDetail()
        {
            var orderId = 10;
            var response = await _httpClient.GetAsync("api/v1/orders/" + orderId);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetOrderDetail_OrderId_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void CancelOrder_OrderId_ReturnOrderDetail()
        {
            var orderId = 10;
            var response = await _httpClient.DeleteAsync("api/v1/orders/" + orderId);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.CancelOrder_OrderId_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void CheckoutWhenUnavailableProductInCart_ReturnBadRequest()
        {
            var productId = 8;
            var request1 = new AddToCartRequest { ProductId = productId, Quantity = 2 };
            var json1 = JsonConvert.SerializeObject(request1, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            var data1 = new StringContent(json1, Encoding.UTF8, "application/json");
            var response1 = await _httpClient.PostAsync("api/v1/carts/add-to-cart", data1);

            var request2 = new CreateOrderRequest { DeliveryInformation = 2, PaymentMethod = "COD" };
            var json2 = JsonConvert.SerializeObject(request2, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            var data2 = new StringContent(json2, Encoding.UTF8, "application/json");
            var response2 = await _httpClient.PostAsync("api/v1/orders", data2);

            var actualResult = await response2.Content.ReadAsStringAsync();
            var expectedResult = _result.CheckoutWhenUnavailableProductInCart_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(!response2.IsSuccessStatusCode);
            Assert.Equal(expected, actual);

            await _httpClient.DeleteAsync("api/v1/carts/remove-product/" + productId);
        }
    }
}

using BookStore.Business.Dto.RequestObjects;
using BookStore.IntegrationTest.ExpectedResults;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BookStore.IntegrationTest.Tests
{
    public class CartControllerTest
    {
        private HttpClient _httpClient;
        private CartControllerExpectedResult _result;
        private string _token = String.Empty;

        public CartControllerTest()
        {
            var factory = new WebApplicationFactory<Program>();
            _httpClient = factory.CreateDefaultClient();
            _result = new CartControllerExpectedResult();
            _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJUaGlzSXNKd3RTdWJqZWN0IiwianRpIjoiMTliZDhlMjUtZTdkNS00ZjA3LTgxMmItNzZhNjgxOTczN2JmIiwiVWlkIjoiMiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkN1c3RvbWVyIiwiZXhwIjoxNjcwMDU4NjIwLCJpc3MiOiJCb29rU3RvcmUiLCJhdWQiOiJCb29rU3RvcmUifQ.b9a4C6Zy2xad2RYxEZpaBrqpUm3UJ2nKDZkPI5kIEUA";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        [Fact]
        public async void AddToCart_ProductId_ReturnTrue()
        {
            var request = new AddToCartRequest { ProductId = 3, Quantity = 2};
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/v1/carts/add-to-cart", data);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.AddToCart_ProductId_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void AddToCart_WrongProductId_ReturnBadRequest()
        {
            var request = new AddToCartRequest { ProductId = 33, Quantity = 2 };
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/v1/carts/add-to-cart", data);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.AddToCart_WrongProductId_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(!response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void IncreaseProductQuantity_ProductId_ReturnCurrentListProductsInCart()
        {
            var productId = 3;
            var response = await _httpClient.PutAsync("api/v1/carts/incease-product/" + productId, null);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.IncreaseProductQuantity_ProductId_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void IncreaseProductQuantity_WrongProductId_ReturnBadRequest()
        {
            var productId = 33;
            var response = await _httpClient.PutAsync("api/v1/carts/incease-product/" + productId, null);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.WrongProductId_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(!response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void DecreaseProductQuantity_ProductId_ReturnCurrentListProductsInCart()
        {
            var productId = 3;
            var response = await _httpClient.PutAsync("api/v1/carts/decease-product/" + productId, null);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.DecreaseProductQuantity_ProductId_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void DecreaseProductQuantity_WrongProductId_ReturnBadRequest()
        {
            var productId = 33;
            var response = await _httpClient.PutAsync("api/v1/carts/decease-product/" + productId, null);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.WrongProductId_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(!response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetCart_ReturnCurrentListProductsInCart()
        {
            var response = await _httpClient.GetAsync("api/v1/carts");

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetCart_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void RemoveProduct_WrongProductId_ReturnBadRequest()
        {
            var productId = 33;
            var response = await _httpClient.DeleteAsync("api/v1/carts/remove-product/" + productId);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.WrongProductId_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(!response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void RemoveProduct_ProductId_ReturnCurrentListProductsInCart()
        {
            var productId = 3;
            var response = await _httpClient.DeleteAsync("api/v1/carts/remove-product/" + productId);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.RemoveProduct_ProductId_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }
    }
}

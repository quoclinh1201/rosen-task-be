
using BookStore.IntegrationTest.ExpectedResults;
using Newtonsoft.Json.Linq;

namespace BookStore.IntegrationTest.Tests
{
    public class ProductsControllerTest
    {
        private HttpClient _httpClient;
        private ProductsControllerExpectedResult _result;

        public ProductsControllerTest()
        {
            var factory = new WebApplicationFactory<Program>();
            _httpClient = factory.CreateDefaultClient();
            _result =  new ProductsControllerExpectedResult();
        }

        [Fact]
        public async void GetListProducts_WithoutParameters_ReturnDefaultListProducts()
        {
            var response = await _httpClient.GetAsync("api/v1/products");

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetListProducts_WithoutParameters_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetListProducts_SearchByNameAndCategory_ReturnListProducts()
        {
            var productName = "a";
            var category = "Sách học ngoại ngữ";
            var response = await _httpClient.GetAsync("api/v1/products?ProductName=" + productName + "&CategoryName=" + category);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetListProducts_SearchByNameAndCategory_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetListProducts_SearchByNameButNotMatch_ReturnEmptyList()
        {
            var productName = "abc";
            var response = await _httpClient.GetAsync("api/v1/products?ProductName=" + productName);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetListProducts_SearchByNameButNotMatch_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetListProducts_SearchByName_ReturnProducts()
        {
            var productName = "bắc âu";
            var response = await _httpClient.GetAsync("api/v1/products?ProductName=" + productName);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetListProducts_SearchByName_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetListProducts_SearchByCategory_ReturnProducts()
        {
            var category = "Tâm lý kỹ năng";
            var response = await _httpClient.GetAsync("api/v1/products?CategoryName=" + category);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetListProducts_SearchByCategory_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetDetailProductById_Id_ReturnSpecificProduct()
        {
            var productId = 5;
            var response = await _httpClient.GetAsync("api/v1/products/" + productId);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetDetailProductById_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetDetailProductById_WrongId_ReturnNotFound()
        {
            var productId = 55;
            var response = await _httpClient.GetAsync("api/v1/products/" + productId);

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetDetailProductById_WrongId_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(!response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }


    }
}

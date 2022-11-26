using BookStore.IntegrationTest.ExpectedResults;
using Newtonsoft.Json.Linq;

namespace BookStore.IntegrationTest.Tests
{
    public class CategoriesControllerTest
    {
        private HttpClient _httpClient;
        private CategoriesControllerExpectedResult _result;

        public CategoriesControllerTest()
        {
            var factory = new WebApplicationFactory<Program>();
            _httpClient = factory.CreateDefaultClient();
            _result = new CategoriesControllerExpectedResult();
        }

        [Fact]
        public async void GetAllCategories_ReturnAlllCategories()
        {
            var response = await _httpClient.GetAsync("api/v1/categories");

            var actualResult = await response.Content.ReadAsStringAsync();
            var expectedResult = _result.GetAllCategories_ExpectedResult;
            var actual = JObject.Parse(actualResult);
            var expected = JObject.Parse(expectedResult);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected, actual);
        }
    }
}

using DataModel;
using IntegrationTestForAPI.Abstraction;
using IntegrationTestForAPI.Classes;
using IntegrationTestForAPI.Static;
using JayanWebAPI;
using JayanWebAPI.Static;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
namespace IntegrationTestForAPI
{
    public class ProductControllerTest : TestCaseUsingToken
    {
      [Fact]
        public async Task TestCreateProduct()
        {
            var token = new TestJwtToken().WithUserName(UserType.TestUserName).Build();
            ProductDataModel productDataModel = new ProductDataModel() { name = "Product1", colour = "Green" };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(UtilityForAPI.JWT_BEARER, token);
            var request = UserType.TestUserName;
            var response2 = await Client.PostAsJsonAsync("/api/Product/Create", productDataModel);
            response2.EnsureSuccessStatusCode();
            var contents = await response2.Content.ReadAsStringAsync();
            Assert.NotEmpty(contents);
         }

        [Theory]
        [InlineData("/api/Product/GetAll")]
        public async Task TestGetProducts(string url)
        {
            var token = new TestJwtToken().WithUserName(UserType.TestUserName).Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(UtilityForAPI.JWT_BEARER, token);
            var request = UserType.TestUserName;
            var response2 = await Client.GetAsync(url);
            response2.EnsureSuccessStatusCode();
            var contents = await response2.Content.ReadAsStringAsync();
            Assert.NotEmpty(contents);
        }

        [Fact]
        public async Task TestGetGreenColorProudcts()
        {
            var token = new TestJwtToken().WithUserName(UserType.TestUserName).Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(UtilityForAPI.JWT_BEARER, token);
            var request = UserType.TestUserName;
            var response2 = await Client.GetAsync("/api/Product/GetAll");
            response2.EnsureSuccessStatusCode();
            var contents = await response2.Content.ReadAsStringAsync();
            Assert.NotEmpty(contents);
        }

        [Theory]
        [InlineData("/api/Product/Get?colour=Green")]
        public async Task Get_SepecificProducts_Only_Test(string url)
        {
            var token = new TestJwtToken().WithUserName(UserType.TestUserName).Build();
            ProductDataModel productDataModel = new ProductDataModel() { name = "Product1", colour = "Green" };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(UtilityForAPI.JWT_BEARER, token);
            var request = UserType.TestUserName;
            var response2 = await Client.PostAsJsonAsync("/api/Product/Create", productDataModel);
            response2.EnsureSuccessStatusCode();
            response2 = await Client.GetAsync(url);
            var contents = await response2.Content.ReadAsStringAsync();
            response2.EnsureSuccessStatusCode();
            Assert.NotEmpty(contents);
        }


        [Fact]
        public async Task User_Should_Not_Be_Able_To_Create_Product_Without_Token()
        {
            ProductDataModel productDataModel = new ProductDataModel() { name = "Product1", colour = "Green" };
            var response2 = await Client.PostAsJsonAsync("/api/Product/Create", productDataModel);
            Assert.Equal("Unauthorized", response2.StatusCode.ToString());
        }


        [Theory]
        [InlineData("/api/Product/GetAll")]
        public async Task User_Should_Not_Be_Able_To_Get_All_Product_Without_Token(string url)
        {
            var response2 = await Client.GetAsync(url);
            Assert.Equal("Unauthorized", response2.StatusCode.ToString());
        }

        [Theory]
        [InlineData("/api/Product/Get?colour=Green")]
        public async Task User_Should_Not_Be_Able_To_Get_Specific_Product_Without_Token(string url)
        {
            var response2 = await Client.GetAsync(url);
            Assert.Equal("Unauthorized", response2.StatusCode.ToString());
        }
       
    }
}
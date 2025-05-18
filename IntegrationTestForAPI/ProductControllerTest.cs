using DataModel;
using IntegrationTestForAPI.Abstraction;
using IntegrationTestForAPI.Classes;
using JayanWebAPI;
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
            var token = new TestJwtToken().WithUserName("testuser").Build();
            ProductDataModel productDataModel = new ProductDataModel() { name = "Product1", colour = "Green" };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var request = "testuser";
            var response2 = await Client.PostAsJsonAsync("/api/Product/Create", productDataModel);
            response2.EnsureSuccessStatusCode();
            var contents = await response2.Content.ReadAsStringAsync();
            Assert.NotEmpty(contents);
         }

        [Theory]
        [InlineData("/api/Product/GetAll")]
        public async Task TestGetProducts(string url)
        {
            var token = new TestJwtToken().WithUserName("testuser").Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var request = "testuser";
            var response2 = await Client.GetAsync(url);
            response2.EnsureSuccessStatusCode();
            var contents = await response2.Content.ReadAsStringAsync();
            Assert.NotEmpty(contents);
        }

        [Fact]
        public async Task TestGetGreenColorProudcts()
        {
            var token = new TestJwtToken().WithUserName("testuser").Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var request = "testuser";
            var response2 = await Client.GetAsync("/api/Product/GetAll");
            response2.EnsureSuccessStatusCode();
            var contents = await response2.Content.ReadAsStringAsync();
            Assert.NotEmpty(contents);
        }

        [Theory]
        [InlineData("/api/Product/Get?colour=Green")]
        public async Task Get_SepecificProducts_Only_Test(string url)
        {
            var token = new TestJwtToken().WithUserName("testuser").Build();
            ProductDataModel productDataModel = new ProductDataModel() { name = "Product1", colour = "Green" };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var request = "testuser";
            var response2 = await Client.PostAsJsonAsync("/api/Product/Create", productDataModel);
            response2.EnsureSuccessStatusCode();
            response2 = await Client.GetAsync(url);
            var contents = await response2.Content.ReadAsStringAsync();
            response2.EnsureSuccessStatusCode();
            Assert.NotEmpty(contents);
        }

           
    }
}
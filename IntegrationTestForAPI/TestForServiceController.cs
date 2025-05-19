using IntegrationTestForAPI.Abstraction;
using IntegrationTestForAPI.Classes;
using IntegrationTestForAPI.Static;
using JayanWebAPI.Static;
using System.Net.Http.Headers;

namespace IntegrationTestForAPI
{
    public class TestForServiceController : TestCaseUsingToken
    {
        [Theory]
        [InlineData("/api/TestService/GetServiceStatus")]
        public async Task Check_If_Service_Is_Running_Without_Authentication(string url)
        {
            var application = new WebAPITestFactory();
            var client = application.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

        }

        [Theory]
        [InlineData("/api/TestService/GetServiceStatus")]
        public async Task Check_If_Service_Is_Running_With_Authentication(string url)
        {
            var application = new WebAPITestFactory();
            var token = new TestJwtToken().WithUserName(UserType.TestUserName).Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(UtilityForAPI.JWT_BEARER, token);
            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();

        }
    }
}

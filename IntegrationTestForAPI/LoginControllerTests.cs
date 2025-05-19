using DataModel;
using FluentAssertions;
using IntegrationTestForAPI.Abstraction;
using IntegrationTestForAPI.Classes;
using IntegrationTestForAPI.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestForAPI
{
    public class LoginControllerTests : TestCaseUsingToken
    {

        [Theory]
        [InlineData("/api/Login/LoginForUser")]
        public async Task Login_Attempt_By_InValid_User(string url)
        {
            var application = new WebAPITestFactory();
            var client = application.CreateClient();
            var request =  new UserDataModel() {UserName= "NotValid" };
            var response = await client.PostAsJsonAsync(url, request);
            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }
      
    }
}

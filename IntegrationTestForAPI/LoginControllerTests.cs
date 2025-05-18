using FluentAssertions;
using IntegrationTestForAPI.Abstraction;
using IntegrationTestForAPI.Classes;
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
       
        [Fact]
        public async Task Check_If_Service_Is_Running()
        {
            var application = new WebAPITestFactory();
            var client = application.CreateClient();
            var response = await client.GetAsync("/api/TestService/GetServiceStatus");
            response.EnsureSuccessStatusCode();
 
        }
      
    }
}

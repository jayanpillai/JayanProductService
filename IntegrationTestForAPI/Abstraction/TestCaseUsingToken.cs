using IntegrationTestForAPI.Static;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace IntegrationTestForAPI.Abstraction
{
    public abstract class TestCaseUsingToken : IAsyncDisposable
    {
        protected readonly WebApplicationFactory<Program> Application;
        protected readonly HttpClient Client;

        protected TestCaseUsingToken()
        {
            Application = new WebApplicationFactory<Program>();
            Application = Application.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.Configure<JwtBearerOptions>(
                        JwtBearerDefaults.AuthenticationScheme,
                        options =>
                        {
                            options.Configuration = new OpenIdConnectConfiguration
                            {
                                Issuer = JwtTokenProvider.Issuer,
                            };
                            options.TokenValidationParameters.ValidIssuer = JwtTokenProvider.Issuer;
                            options.TokenValidationParameters.ValidAudience = JwtTokenProvider.Issuer;
                            options.Configuration.SigningKeys.Add(JwtTokenProvider.SecurityKey);
                        }
                    );
                });
            });
            Client = Application.CreateClient();
        }

        public async ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);
            await Application.DisposeAsync();
        }
    }
}

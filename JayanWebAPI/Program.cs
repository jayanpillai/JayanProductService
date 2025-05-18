using DataModel;
using DataService;
using DataService.ConcreteClasses;
using DataService.Interfaces;
using InMemoryRepo.ConcreteClasses;
using InMemoryRepo.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.ConcreteClasses;
using Repository.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IProductInMemoryRepo, ProductMemoryRepo>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IValidUserAccountRepo, ValidUserAccountRepo>();
builder.Services.AddSingleton<IUserService, UserService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region configuring swagger to enable token authorization
builder.Services.AddSwaggerGen(options =>

{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Enter Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }

        }
    });
});

#endregion


#region jwt based authentication and authroization  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
}

);

#endregion



var app = builder.Build();



#region Loading valid users

List<UserDataModel> validUsers = builder.Configuration.GetSection("ValidUsers:ServiceAccounts").Get<List<UserDataModel>>();

var validServiceAccounts = app.Services.GetRequiredService<IUserService>();
foreach (var item in validUsers)
{
    validServiceAccounts.add(item);
}

#endregion


app.UseAuthentication();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#region This allows us to use program services in Integration Testing
public partial class Program { }
#endregion

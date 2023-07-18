

// https://engineering.hexacta.com/how-to-implement-graphql-in-asp-net-core-web-api-net-6-using-hotchocolate-a30b57b8a989
// https://github.com/Tech-With-Christian/SuperHeroApi/tree/master
// https://github.com/MPonceG/GraphQLNetCoreHotChocolate/blob/main/Scripts/Create_Data_StoreDB.sql
// https://www.learmoreseekmore.com/2022/01/basic-graphql-crud-operation-dotnet6-hotchocolate-v12-sqldatabase.html
// https://dev.to/jioophoenix/hotchocolate-introduction-to-graphql-for-asp-net-core-part-1-2e27

// further reference
// https://jscrambler.com/blog/building-a-crud-app-with-vue-and-graphql

// Authentication
//https://www.infoworld.com/article/3669188/how-to-implement-jwt-authentication-in-aspnet-core-6.html

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Auth;
using WebAPI.Data;
using WebAPI.Data.GraphQL;
using WebAPI.Data.Repositories;
using WebAPI.Models;
using WebAPI.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ActivityRepository, ActivityRepository>();
builder.Services.AddScoped<UserRepository, UserRepository>();
builder.Services.AddScoped<ResultRepository, ResultRepository>();

builder.Services.AddControllers();

builder.Services.AddDbContext<StoreDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("StoreDB")));

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>();

builder.Services.AddHostedService<MqttSubscriber>();
//Add Cors
builder.Services.AddCors();

//Authentication Scheme
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

builder.Services.AddSingleton(new TokenService(builder.Configuration["Jwt:Key"],builder.Configuration["Jwt:Issuer"], builder.Configuration["Jwt:Audience"]));

var app = builder.Build();

// HTTP Get endpoint to test Authentication and Authorization -- TODO : delete this endpoint
app.MapGet("/security/getMessage",
(ClaimsPrincipal claims) => "Hello World! " + DateTime.Now.ToString("hh:mm:ss tt") + " " + claims.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp))
    .RequireAuthorization();

app.MapPost("/security/createToken", [AllowAnonymous] (User user) => 
{
    if(user.UserId == 1)
    {
        var ts = new TokenService(builder.Configuration["Jwt:Key"], builder.Configuration["Jwt:Issuer"], builder.Configuration["Jwt:Audience"]);
        var token = ts.GenerateToken(user.Email);
        return Results.Ok(token);
    }
    return Results.Unauthorized();
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseWebSockets();
app.MapGraphQL();
app.MapControllers();

app.Run();

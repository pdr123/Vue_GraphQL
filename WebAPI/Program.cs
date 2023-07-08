

// https://engineering.hexacta.com/how-to-implement-graphql-in-asp-net-core-web-api-net-6-using-hotchocolate-a30b57b8a989
// https://github.com/Tech-With-Christian/SuperHeroApi/tree/master
// https://github.com/MPonceG/GraphQLNetCoreHotChocolate/blob/main/Scripts/Create_Data_StoreDB.sql
// https://www.learmoreseekmore.com/2022/01/basic-graphql-crud-operation-dotnet6-hotchocolate-v12-sqldatabase.html
// https://dev.to/jioophoenix/hotchocolate-introduction-to-graphql-for-asp-net-core-part-1-2e27

using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.GraphQL;
using WebAPI.Data.Repositories;
//using GraphqlCoreEmptyChocolate.Data.Repositories;
//using GraphqlCoreEmptyChocolate.Data;
//using GraphqlCoreEmptyChocolate.Data.GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ActivityRepository, ActivityRepository>();
builder.Services.AddScoped<UserRepository, UserRepository>();
builder.Services.AddScoped<ResultRepository, ResultRepository>();

builder.Services.AddControllers();

builder.Services.AddDbContext<StoreDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("StoreDB")));

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>();

//Add Cors
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

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

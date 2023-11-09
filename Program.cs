using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using SaginEmployees;
using SaginEmployees.Models;
using SaginEmployees.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.Configure<MongoDbConnection>(
    builder.Configuration.GetSection("MongoDb"));

builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(
    s.GetRequiredService<IOptions<MongoDbConnection>>().Value.ConnectionString));
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSingleton<JwtTokenHandlerService>();
// builder.Services.AddAuthentication().AddJwtBearer(options =>
// {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuerSigningKey = true,
//         ValidateAudience = false,
//         ValidateIssuer = false,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
//             builder.Configuration.GetSection("Jwt:Key").Value!))
//     };
// });
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.Run();

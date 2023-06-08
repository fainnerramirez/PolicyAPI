using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Policies.Models;
using System.Text;
using Policies.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Dentro del método ConfigureServices
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });

//policy services
builder.Services.Configure<PoliciesSettings>(
    builder.Configuration.GetSection(nameof(PoliciesSettings)));

builder.Services.AddSingleton<IPoliciesSettings>(
    a => a.GetRequiredService<IOptions<PoliciesSettings>>().Value);


builder.Services.AddSingleton<IMongoClient>(
    a => new MongoClient(builder.Configuration.GetValue<string>("PoliciesSettings:ConnectionString")));

//user sercvices
builder.Services.Configure<UserSettings>(
    builder.Configuration.GetSection(nameof(UserSettings)));

builder.Services.AddSingleton<IUserSettings>(
    a => a.GetRequiredService<IOptions<UserSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(
    a => new MongoClient(builder.Configuration.GetValue<string>("UserSettings:ConnectionString")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

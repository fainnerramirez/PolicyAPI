using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Policies.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Policies.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IConfiguration _config;

        public UserService(UserSettings settings, IConfiguration config, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
            _config = config;
        }

        public User Create(User user)
        {
            _users.InsertOne(user);

            return user;
        }

        public bool AuthenticateUser(string username, string password)
        {
            var user = _users.Find(user => user.UserName == username && user.Password == password).FirstOrDefault();
            return user != null;
        }

        public string GenerateToken(string username)
        {
            //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            //var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var jwtSettings = _config.GetSection("Jwt");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var expires = DateTime.UtcNow.AddHours(1);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256
                )
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public User Get(string id)
        {
            return _users.Find(user => user.Id == id).FirstOrDefault();
        }

        public List<User> GetAllUsers()
        {
            return _users.Find(user => true).ToList();
        }
    }
}
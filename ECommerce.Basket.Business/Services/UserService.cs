using ECommerce.Basket.Business.Constants;
using ECommerce.Basket.Business.Services.Abstract;
using ECommerce.Basket.Data.Entities;
using ECommerce.Basket.Models;
using ECommerce.Basket.Models.InfrastuctureModels;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Basket.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IAppSettings _appSettings;
        private readonly IMongoCollection<User> _userCollection;
        private readonly ILogger<UserService> _logger;

        public UserService(IMongoClient client, IECommerceDatabaseSettings settings, IAppSettings appSettings, ILogger<UserService> logger)
        {
            _userCollection = client.GetDatabase(settings.DatabaseName).GetCollection<User>(CollectionConstants.UserCollection);
            _appSettings = appSettings;
            _logger = logger;
        }

        public async Task<TokenModel> Login(User user)
        {
            user = await _userCollection.Find(p => p.Username == user.Username && p.Password == user.Password).SingleOrDefaultAsync();
            if (user == null) return null;
            var tokenModel = GenerateJwtToken(user.Id);

            return tokenModel;
        }

        private TokenModel GenerateJwtToken(string userId)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenModel = new TokenModel();
            tokenModel.Expiration = DateTime.Now.AddDays(1);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _appSettings.Issuer,
                audience: _appSettings.Audience,
                expires: tokenModel.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: new List<Claim>() { new Claim("id", userId) }
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenModel.Id = userId;
            _logger.LogInformation("{@userId} token created and user logged in {@tokenModel}", new object[] { userId, tokenModel });
            return tokenModel;
        }
    }
}

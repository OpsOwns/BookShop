using System;
using System.Security.Cryptography;
using Shop.Auth.Application.Security.Jwt.Interfaces;

namespace Shop.Auth.Application.Security.Jwt
{
    public class TokenFactory : ITokenFactory
    {
        public string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}

﻿namespace Shop.Auth.Infrastructure.Security.Jwt.Interfaces
{
    public interface ITokenFactory
    {
        string GenerateToken(int size = 32);
    }
}
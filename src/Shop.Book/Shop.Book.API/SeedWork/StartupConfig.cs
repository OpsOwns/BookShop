﻿using Microsoft.Extensions.Configuration;
using Shop.Shared.API;
using Shop.Shared.Model;

namespace Shop.Book.API.SeedWork
{
    public class StartupConfig
    {
        public AuthOption AuthSettings { get; }
        public StartupConfig(IConfiguration configuration)
        {
            AuthSettings = configuration.GetOptions<AuthOption>("AuthSettings");
        }
    }
}
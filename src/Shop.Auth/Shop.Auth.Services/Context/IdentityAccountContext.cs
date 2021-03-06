﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Auth.Services.Domain;
using Shop.Shared.Model;

namespace Shop.Auth.Services.Context
{
    public class IdentityAccountContext : IdentityDbContext<ShopUser, ShopRole, int>
    {
        private DatabaseOption DatabaseOption { get; }
        private readonly ILoggerFactory _loggerFactory;

        public IdentityAccountContext(DbContextOptions<IdentityAccountContext> options, DatabaseOption databaseOption,
            ILoggerFactory loggerFactor)
            : base(options)
        {
            DatabaseOption = databaseOption;
            _loggerFactory = loggerFactor;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (DatabaseOption.InMemmory)
            {
                optionsBuilder.UseInMemoryDatabase("Auth");
                return;
            }
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.UseSqlServer(DatabaseOption.ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("Logins");
            builder.Entity<IdentityUserToken<int>>().ToTable("Tokens");
            builder.Entity<ShopRole>().ToTable("ShopUserRoles");
            builder.Entity<ShopUser>().ToTable("ShopUser");
        }
    }
}

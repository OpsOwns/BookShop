﻿using Dawn;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Formatting.Compact;
using Shop.Shared.API.Version;
using Shop.Shared.Domain;
using Shop.Shared.Domain.Event;
using Shop.Shared.Model;
using Shop.Shared.Shared;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using ValidationProblemDetails = Shop.Shared.Shared.ValidationProblemDetails;

namespace Shop.Shared.API
{
    public static class ExtensionsApi
    {
        private static ILogger Logger { get; set; }

        public static IServiceCollection AddWebApi<T>(this IServiceCollection services) where T : class
        {
            Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.RollingFile(new CompactJsonFormatter(), "logs/log.json")
                .CreateLogger();
            services.AddControllers(options =>
                options.RespectBrowserAcceptHeader = true).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
            services.Configure<KestrelServerOptions>(o => o.AllowSynchronousIO = true);
            services.Configure<IISServerOptions>(o => o.AllowSynchronousIO = true);
            services.AddMvcCore();
            services.AddLogging(x => x.AddSerilog(Logger));
            services.AddProblemDetails(details =>
                details.Map<ValidationException>(exception => new ValidationProblemDetails(exception)));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            return services;
        }
        public static IServiceCollection AddValidation<T>(this IServiceCollection services) where  T : class => services.AddValidatorsFromAssembly(typeof(T).Assembly);
        public static IServiceCollection AddDomainEvents(this IServiceCollection services)
        {
            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
            return services;
        }
        public static IServiceCollection AddSwagger<T>(this IServiceCollection services, IConfiguration configuration,
            bool security = false)
            where T : class
        {
            var swaggerOptions = configuration.GetOptions<SwaggerOption>("Swagger");
            Guard.Argument(() => swaggerOptions)
                .Member(x => x.Description, z => z.NotNull())
                .Member(x => x.Name, z => z.NotNull().NotEmpty())
                .Member(x => x.Name, z => z.NotNull().NotEmpty())
                .Member(x => x.Title, z => z.NotNull().NotEmpty())
                .Member(x => x.Version, z => z.NotNull().NotEmpty())
                .Member(x => x.EndPoint, z => z.NotNull().NotEmpty());
            services.AddSwaggerGen(options =>
            {
                options.ExampleFilters();
                options.SwaggerDoc(swaggerOptions.Name, new OpenApiInfo
                {
                    Description = swaggerOptions.Description,
                    Version = swaggerOptions.Version,
                    Title = swaggerOptions.Title
                });
                if (security)
                {
                    options?.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = JwtBearerDefaults.AuthenticationScheme
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = JwtBearerDefaults.AuthenticationScheme,
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
                    });
                }

                options.ResolveConflictingActions(x => x.First());
                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
            });
            services.AddSwaggerExamplesFromAssemblyOf<T>();
            services.Configure<SwaggerOptions>(c => c.SerializeAsV2 = true);
            return services;
        }
        public static IServiceCollection AddJwt(this IServiceCollection services, AuthOption authOption)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOption.SecretKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = authOption.Audience,
                    ValidIssuer = authOption.Issuer,
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }

        public static IServiceCollection AddEfCore<T>(this IServiceCollection services, IConfiguration configuration,
            string configName) where T : DbContext
        {
            Guard.Argument(configName, nameof(configName)).NotEmpty().NotNull();
            services.AddSingleton(configuration.GetOptions<DatabaseOption>(configName));
            services.AddEntityFrameworkSqlServer().AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<T>(ServiceLifetime.Transient);
            return services;
        }
        public static IServiceCollection AddMapper<T>(this IServiceCollection services) where T : class
        {
            services.AddAutoMapper(typeof(T));
            return services;
        }
        public static IServiceCollection AddRepository<T>(this IServiceCollection serviceCollection) where T : class
        {
            serviceCollection.Scan(scan =>
            {
                scan.FromAssemblies(typeof(T).Assembly)
                    .AddClasses(classes => classes.AssignableTo<IRepository>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });
            return serviceCollection;
        }

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
            where TModel : new()
        {
            var model = new TModel();
            configuration
                .GetSection(sectionName)
                .Bind(model);
            return model;
        }
    }
}
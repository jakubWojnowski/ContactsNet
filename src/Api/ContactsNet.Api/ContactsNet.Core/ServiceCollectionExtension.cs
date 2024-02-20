using System.Runtime.CompilerServices;
using ContactsNet.Core.Authentication;
using ContactsNet.Core.Contexts;
using ContactsNet.Core.Dal.Entities;
using ContactsNet.Core.Dal.Extensions.MsSql;
using ContactsNet.Core.Dal.Persistence;
using ContactsNet.Core.Dal.Repositories;
using ContactsNet.Core.Dto;
using ContactsNet.Core.Middlewares;
using ContactsNet.Core.Policies;
using ContactsNet.Core.Services;
using ContactsNet.Core.Validations.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

[assembly: InternalsVisibleTo("ContactsNet.Api")]

namespace ContactsNet.Core;

internal static class ServiceCollectionExtension
{
    private const string CorsPolicy = "cors";

    internal static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(cors =>
        {
            cors.AddPolicy(CorsPolicy, x =>
            {
                x.WithOrigins("http://localhost:3000")
                    .WithMethods("POST","GET", "PUT", "DELETE", "PATCH")
                    .WithHeaders("Content-Type", "Authorization")
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("Location");
            });
        });
        services.AddSwaggerGen(swagger =>
        {
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ContactsNet API",
                Version = "v1"
            });
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
  
        services.AddErrorHandling();
        services.AddMsSql();
        services.AddMsSql<ContactsNetDbContext>();
        services.AddTokenAuthentication(configuration);
        services.AddScoped<IContext, Context>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserContactRepository, UserContactRepository>();
        services.AddScoped<IUserService , UserService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddTransient<IValidator<RegisterUserDto>, RegisterUserValidator>();
        services.AddTransient<IValidator<LoginUserDto>, LoginUserValidator>();
        services.AddTransient<IValidator<UserDto>, UpdateUserValidator>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserContactService, UserContactService>();
       services.AddScoped<ICannotOperateOnContact, CannotOperateOnContact>();
       services.AddScoped<ICannotAddContact, CannotAddContact>();
        services.AddControllers();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }

    public static IApplicationBuilder UseCore(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicy);
        app.UseErrorHandling();
        app.UseSwagger();
        app.UseSwaggerUI(swagger =>
        {
            swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactsNet API");
            swagger.RoutePrefix = string.Empty;
        });
        return app;
    }


    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }

    private static T GetOptions<T>(this IConfiguration configuration, string sectionName)
        where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}
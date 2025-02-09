﻿using Hangfire;
using Microsoft.OpenApi.Models;
using NLog;
using ILogger = NLog.ILogger;

namespace Oid85.HomeBot.WebHost.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureLogger(this IServiceCollection services)
    {
        LogManager
            .Setup()
            .LoadConfigurationFromFile("nlog.config");

        services.AddTransient(typeof(ILogger), _ => 
            LogManager.GetLogger(AppDomain.CurrentDomain.FriendlyName));
    }

    public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Api",
                Description = AppDomain.CurrentDomain.FriendlyName
            });

            c.IncludeXmlComments(GetXmlCommentsPath());
        });
    }

    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.SetIsOriginAllowed(_ => true);
                builder.AllowCredentials();
            });
        });
    }

    public static void ConfigureHangfire(
        this IServiceCollection services)
    {
        services.AddHangfire(config => config
            .UseInMemoryStorage());
            
        services.AddHangfireServer();
    }
        
    public static void AddFactory<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        services.AddTransient<TService, TImplementation>();
        services.AddSingleton<Func<TService>>(x => () => x.GetService<TService>()!);
    }

    private static string GetXmlCommentsPath()
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SwaggerTest.XML");
    }
}
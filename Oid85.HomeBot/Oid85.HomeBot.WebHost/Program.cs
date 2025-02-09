using Hangfire;
using Oid85.HomeBot.Application.Extensions;
using Oid85.HomeBot.Common.KnownConstants;
using Oid85.HomeBot.DataAccess.Extensions;
using Oid85.HomeBot.External.Extensions;
using Oid85.HomeBot.WebHost.Extensions;

namespace Oid85.HomeBot.WebHost;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
            
        builder.Services.AddControllers();
        builder.Services.AddMemoryCache();
        builder.Services.ConfigureLogger();
        builder.Services.ConfigureSwagger(builder.Configuration);
        builder.Services.ConfigureCors(builder.Configuration);            
        builder.Services.ConfigureApplicationServices();
        builder.Services.ConfigureExternalServices(builder.Configuration);
        builder.Services.ConfigureFinMarketDataAccess(builder.Configuration);
        builder.Services.ConfigureHangfire();
            
        builder.Services.AddWindowsService(options =>
        {
            options.ServiceName = "Oid85.HomeBot";
        });
        
        bool applyMigrations = builder.Configuration.GetValue<bool>(KnownSettingsKeys.PostgresApplyMigrationsOnStart);
        int port = builder.Configuration.GetValue<int>(KnownSettingsKeys.DeployPort);
        
        var app = builder.Build();
        
        if (applyMigrations) 
            await app.ApplyMigrations();

        await app.TelegramBotSubscribe();
        
        app.UseRouting();

        app.UseCors("CorsPolicy");

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = "";
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1");
        });

        app.UseHangfireDashboard("/dashboard");
        
        await app.RegisterHangfireJobs(builder.Configuration);
        
        app.MapControllers();
        
        app.Urls.Add($"http://0.0.0.0:{port}");
        
        await app.RunAsync();
    }
}
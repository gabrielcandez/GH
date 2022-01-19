using GH.Application;
using Microsoft.EntityFrameworkCore;

namespace GH.Api.Configuration;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(services =>
        {
            var options = services.GetService<Options>()!.Database;
            return new DatabaseContext(options.Host, options.User, options.Password, options.Database);
        });

        builder.Services.AddHostedService<DatabaseInitializerService>();
    }

    private sealed class DatabaseInitializerService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseInitializerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<DatabaseContext>()!;

            await context.Database.MigrateAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
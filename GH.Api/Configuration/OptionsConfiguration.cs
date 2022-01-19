using Microsoft.Extensions.Options;

namespace GH.Api.Configuration;

public static class OptionsConfiguration
{
    public static void ConfigureOptions(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddEnvironmentVariables("GH_");
        builder.Services.Configure<Options>(builder.Configuration);
        builder.Services.AddSingleton(services => services.GetService<IOptions<Options>>()!.Value);
    }
}

public sealed class Options
{
    public DatabaseOptions Database { get; set; }

    public sealed class DatabaseOptions
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
    }
}
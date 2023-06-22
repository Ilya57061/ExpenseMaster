using Serilog;

namespace ExpenseMaster.Configuration
{
    public static class LoggingExtensions
    {
        public static void AddCustomLogging(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"logs/log_{DateTime.Now:yyyyMMdd}.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog();
            });
        }
    }
}

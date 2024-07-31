using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;

namespace CarAuction.App.Extensions
{
    public static class SerilogExtension
    {
        public static void AddSerilogExtension(this IHostBuilder builder, string connectionString)
        {
            Logger logger = new LoggerConfiguration()
              .WriteTo.MSSqlServer(
               connectionString: connectionString,
               sinkOptions: new MSSqlServerSinkOptions()
               {
                   AutoCreateSqlDatabase = true,
                   TableName = "Logs"
               })
              .Enrich.FromLogContext()
            .MinimumLevel.Information()
              .CreateLogger();

            builder.UseSerilog(logger);
        }
    }
}

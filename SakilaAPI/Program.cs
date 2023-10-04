using Microsoft.AspNetCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace SakilaAPI
{
    /// <summary>
    /// Program
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// main run application
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = BuilderWebHost(args);
            builder.Build().Run();
        }

<<<<<<< HEAD
        static IWebHostBuilder BuilderWebHost(string[] args)=>
            WebHost.CreateDefaultBuilder(args)
                    .ConfigureLogging(logging =>
                    {
                        //revmove provider loging
                        logging.ClearProviders();
                        //add logging with console
                        logging.AddConsole();
                    })
                    .UseStartup<Startup>();
=======
        [Obsolete]
        public static IWebHostBuilder BuilderWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseSerilog((context, config) =>
                    {
                        config
                            .MinimumLevel.Debug()
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                            .MinimumLevel.Override("System", LogEventLevel.Warning)
                            .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                            .Enrich.FromLogContext()
                            .WriteTo.File(@"Sakira_log.txt")
                            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate);
                    });
        }
>>>>>>> 101f70ef9824a639cdf251d6c3d58e415c903b47
    }
}
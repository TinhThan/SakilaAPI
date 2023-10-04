using Microsoft.AspNetCore;

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
    }
}
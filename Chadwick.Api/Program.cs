using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Chadwick.Api
{
    /// <summary>
    /// Program
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static async Task Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();
            
            using (var scope = webHost.Services.CreateScope())
            {
                // get the IpPolicyStore instance
                var ipPolicyStore = scope.ServiceProvider.GetRequiredService<IIpPolicyStore>();

                // seed IP data from appsettings
                await ipPolicyStore.SeedAsync();
            }

            await webHost.RunAsync();
        }

        /// <summary>
        /// Host Builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
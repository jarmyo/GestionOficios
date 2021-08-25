[assembly: HostingStartup(typeof(GestionWeb.Areas.Identity.IdentityHostingStartup))]
namespace GestionWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
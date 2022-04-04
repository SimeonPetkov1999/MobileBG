[assembly: HostingStartup(typeof(MobileBG.Web.Areas.Identity.IdentityHostingStartup))]

namespace MobileBG.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}

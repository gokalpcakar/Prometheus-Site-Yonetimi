using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus.API.Areas.Identity.Data;
using Prometheus.API.Data;

[assembly: HostingStartup(typeof(Prometheus.API.Areas.Identity.IdentityHostingStartup))]
namespace Prometheus.API.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityPrometheusContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityPrometheusContextConnection")));

                services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<IdentityPrometheusContext>();
            });
        }
    }
}
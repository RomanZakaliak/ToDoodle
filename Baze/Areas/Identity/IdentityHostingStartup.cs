using System;
using Baze.Areas.Identity.Data;
using Baze.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Baze.Areas.Identity.IdentityHostingStartup))]
namespace Baze.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BazeContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BazeContextConnection")));

                services.AddDefaultIdentity<BazeUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<BazeContext>();
            });
        }
    }
}
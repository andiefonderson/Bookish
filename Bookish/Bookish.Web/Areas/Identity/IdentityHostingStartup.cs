using System;
using Bookish.Web.Areas.Identity.Data;
using Bookish.Web.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Bookish.Web.Areas.Identity.IdentityHostingStartup))]
namespace Bookish.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BookishWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BookishWebContextConnection")));

                services.AddDefaultIdentity<BookishWebUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<BookishWebContext>();
            });
        }
    }
}
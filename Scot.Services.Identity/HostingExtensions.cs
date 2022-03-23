using Duende.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scot.Services.Identity.Data;
using Scot.Services.Identity.Initializer;
using Scot.Services.Identity.Models;
using Serilog;

namespace Scot.Services.Identity
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //builder.Services.AddScoped<IDbInitializer, DbInitializer>();

            builder.Services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;

                    // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                    options.EmitStaticAudienceClaim = true;

                    //KEY management - see https://docs.duendesoftware.com/identityserver/v6/fundamentals/keys/
                    // set path where to store keys
                    //options.KeyManagement.KeyPath = "/home/shared/keys";

                    // new key every 30 days
                    //options.KeyManagement.RotationInterval = TimeSpan.FromDays(30);

                    // announce new key 2 days in advance in discovery
                    //options.KeyManagement.PropagationTime = TimeSpan.FromDays(2);

                    // keep old key for 7 days in discovery for validation of tokens
                    //options.KeyManagement.RetentionDuration = TimeSpan.FromDays(7);
                })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<ApplicationUser>();

            //builder.Services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            //        // register your IdentityServer with Google at https://console.developers.google.com
            //        // enable the Google+ API
            //        // set the redirect URI to https://localhost:5001/signin-google
            //        options.ClientId = "copy client ID from Google here";
            //        options.ClientSecret = "copy client secret from Google here";
            //    });

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app/*, IDbInitializer dbInitializer*/)
        {
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            //dbInitializer.Initialize();

            app.MapRazorPages()
                .RequireAuthorization();

            return app;
        }
    }
}
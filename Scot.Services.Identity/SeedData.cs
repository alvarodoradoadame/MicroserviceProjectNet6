using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scot.Services.Identity.Data;
using Scot.Services.Identity.Models;
using Serilog;
using System.Security.Claims;

namespace Scot.Services.Identity
{
    public class SeedData
    {
        public static void EnsureSeedData(WebApplication app)
        {
            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (roleMgr.FindByIdAsync(Config.Admin).Result == null)
                {
                    roleMgr.CreateAsync(new IdentityRole(Config.Admin)).GetAwaiter().GetResult();
                    roleMgr.CreateAsync(new IdentityRole(Config.Customer)).GetAwaiter().GetResult();
                }

                var alice = userMgr.FindByNameAsync("alice").Result;
                if (alice == null)
                {
                    alice = new ApplicationUser
                    {
                        UserName = "alice",
                        Email = "AliceSmith@email.com",
                        EmailConfirmed = true,
                    };
                    var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                            new Claim(JwtClaimTypes.Role, Config.Customer)
                        }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Log.Debug("alice created");
                }
                else
                {
                    Log.Debug("alice already exists");
                }

                var bob = userMgr.FindByNameAsync("bob").Result;
                if (bob == null)
                {
                    bob = new ApplicationUser
                    {
                        UserName = "bob",
                        Email = "BobSmith@email.com",
                        EmailConfirmed = true
                    };
                    var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim(JwtClaimTypes.Role, Config.Customer),
                            new Claim("location", "somewhere")
                        }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Log.Debug("bob created");
                }
                else
                {
                    Log.Debug("bob already exists");
                }

                var admin = userMgr.FindByNameAsync("admin").Result;
                if (admin == null)
                {
                    admin = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@scotrestaurant.com",
                        EmailConfirmed = true,
                        PhoneNumber = "555-55-5555",
                        FirstName = "Ben",
                        LastName = "Admin"
                    };
                    var result = userMgr.CreateAsync(admin, "Admin123*").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(admin, new Claim[]{
                            new Claim(JwtClaimTypes.Name, admin.FirstName + " " + admin.LastName),
                            new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                            new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                            new Claim(JwtClaimTypes.WebSite, $"http://{admin.FirstName}.com"),
                            new Claim(JwtClaimTypes.Role, Config.Admin),
                            new Claim("location", "here")
                        }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Log.Debug($"{admin.FirstName} created");
                }
                else
                {
                    Log.Debug($"{admin.FirstName} already exists");
                }
            }
        }
    }
}
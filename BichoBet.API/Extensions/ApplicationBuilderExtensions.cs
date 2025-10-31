using BichoBet.Domain.Entities;
using BichoBet.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BichoBet.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseApplicationPipeline(this WebApplication app)
    {
        var forwardedHeadersOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        };
        
        forwardedHeadersOptions.KnownNetworks.Clear();
        forwardedHeadersOptions.KnownProxies.Clear();
        
        app.UseForwardedHeaders(forwardedHeadersOptions);
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BichoBet API v1");
            });
        }
        
        //app.UseHttpsRedirection();
        
        app.UseCors("_myAllowSpecificOrigins");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }

    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        
        var db = services.GetRequiredService<ApplicationDbContext>();
        await db.Database.MigrateAsync();
        
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var roles = new[] { "Customer", "Admin", "Support" };
        
        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        const string adminEmail = "admin@demo.local";
        const string adminPassword = "Admin123!"; // meets default Identity password requirements
        
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FullName = "Administrator"
            };

            var createResult = await userManager.CreateAsync(adminUser, adminPassword);
            if (!createResult.Succeeded)
            {
                // If creation fails (e.g., password policy), throw to make the issue visible early in dev
                var errors = string.Join(", ", createResult.Errors.Select(e => $"{e.Code}:{e.Description}"));
                throw new InvalidOperationException($"Failed to create admin user: {errors}");
            }
        }
        
        // Ensure admin is in Admin role
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}
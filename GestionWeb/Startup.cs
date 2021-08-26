using GestionWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace GestionWeb;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        Core.ConnectionString = Configuration["gestionDatosConnectionString"];
        services.AddDbContext<GestionOficiosContext>(options =>
                       options.UseSqlServer(Configuration["gestionDatosConnectionString"])
                       .UseLazyLoadingProxies()
                       );

        services.AddDbContext<NucleoCompartidoContext>(options =>
            options.UseSqlServer(Configuration["gestionSeguridadConnectionString"]));
        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<NucleoCompartidoContext>();
        services.AddRazorPages();
        //services.AddMvc();
        var mvcBuilder = services.AddControllersWithViews();
        mvcBuilder.AddRazorRuntimeCompilation();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
        }
        else
        {
            app.UseHsts();
        }
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
             name: "areas",
             pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}/{offset?}"
             );

            endpoints.MapControllerRoute(
            name: "controllers",
            pattern: "{controller=Home}/{action=Index}/{id?}/{offset?}"
            );
            endpoints.MapRazorPages();
        });
    }
}
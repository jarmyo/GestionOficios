using GestionWeb.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GestionWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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
            services.AddMvc();
            var mvcBuilder = services.AddControllersWithViews();            
            mvcBuilder.AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              //  app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

          //  app.UseHttpsRedirection();
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
}

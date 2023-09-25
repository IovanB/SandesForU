using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SandesForU.Areas.Admin.Services;
using SandesForU.Context;
using SandesForU.Models;
using SandesForU.Repositories;
using SandesForU.Repositories.Interfaces;
using SandesForU.Services;

namespace SandesForU;
public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>

        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Home/AccessDenied");

        services.Configure<ConfigurationImages>(Configuration.GetSection("ConfigurationPastaImagens"));

        services.AddAuthorization(options =>
        options.AddPolicy("Admin", policy =>
        {
            policy.RequireRole("Admin");
        }));

        services.AddTransient<ILancheRepository, LancheRepository>();
        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        services.AddTransient<IPedidoRepository, PedidoRepository>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped(sp => CarrinhoCompra.GetCarrinhoCompra(sp));
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        services.AddScoped<RelatorioVendasService>();

        services.AddControllersWithViews();

        services.AddPaging(options=> {
            options.ViewName = "Bootstrap4";
            options.PageParameterName = "pageindex";
        } );

        services.AddMemoryCache();
        services.AddSession();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();


        }
        else
        {
            app.UseExceptionHandler("Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        seedUserRoleInitial.SeedRoes();
        seedUserRoleInitial.SeedUsers();

        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
         );

            endpoints.MapControllerRoute(
                name: "categoriaFiltro",
                pattern: "Lanche/{action}/{categoria?}",
                defaults: new { Controller = "Lanche", action = "List" });


            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}

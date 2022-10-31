using AgileTask.AutoMapper;
using AgileTask.Domain.Contexts;
using AgileTask.Domain.Contracts.Repositories;
using AgileTask.Domain.Contracts.Services;
using AgileTask.Domain.Entities;
using AgileTask.Repositories;
using AgileTask.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AgileTask
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

            #region AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new RoleProfile());
                mc.AddProfile(new PositionProfile());
                mc.AddProfile(new DepartmentProfile());
                mc.AddProfile(new VacationApplicationProfile());
                mc.AddProfile(new VacationDaysProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion


            services.AddControllersWithViews();
            services.AddScoped<IRepository, EfRepository>();
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped<IServiceFactory, ServiceFactory>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();


            #region Db
            services.AddScoped<AgileDbContext>();
            services.AddDbContext<AgileDbContext>((option) => option.UseSqlServer(Configuration.GetSection("ConnectionStrings").GetValue<string>("DBConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(
              option =>
              {
                  option.Password.RequireUppercase = false;
                  option.Password.RequireNonAlphanumeric = false;


              }).AddEntityFrameworkStores<AgileDbContext>().AddDefaultTokenProviders();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

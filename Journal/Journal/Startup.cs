using Journal.AutoMapper;
using Journal.Dto.Get;
using Journal.Models;
using Journal.Repositories;
using Journal.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Journal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(connection));
            services.AddScoped<IAsyncRepository<Department>, AsyncRepository<Department>>();
            services.AddScoped<IAsyncRepository<Person>, AsyncRepositoryPerson>();
            services.AddScoped<IAsyncRepositoryWorkLog<WorkLog>, AsyncRepositoryWorkLog>();
            services.AddScoped<IAsyncService<Department,GetDepartmentDto>, AsyncServiceDepartment>();
            services.AddScoped<IAsyncService<Person, GetPersonDto>, AsyncServicePerson>();
            services.AddScoped<IAsyncServiceWorkLog<WorkLog, GetWorkLogTableDto,GetDataChartDto>, AsyncServiceWorkLog>();
        }

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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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

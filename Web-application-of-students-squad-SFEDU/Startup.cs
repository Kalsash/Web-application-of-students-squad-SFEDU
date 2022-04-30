using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_application_of_students_squad_SFEDU.Models;


namespace Web_application_of_students_squad_SFEDU
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<IUserValidator<User>, CustomUserValidator>();

            services.AddTransient<ArticlesRepository>();
            services.AddTransient<ContactsRepository>();
            services.AddTransient<JoinRepository>();
            services.AddTransient<GaleryRepository>();

            services.AddTransient<IPasswordValidator<User>,
            CustomPasswordValidator>(serv => new CustomPasswordValidator(6));

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 6;   // ����������� �����
                opts.Password.RequireNonAlphanumeric = true;   // ��������� �� �� ���������-�������� �������
                opts.Password.RequireLowercase = true; // ��������� �� ������� � ������ ��������
                opts.Password.RequireUppercase = true; // ��������� �� ������� � ������� ��������
                opts.Password.RequireDigit = true; // ��������� �� �����
                opts.User.RequireUniqueEmail = true;    // ���������� email
                opts.User.AllowedUserNameCharacters = ".@abcdefghijklmnopqrstuvwxyz"; // ���������� �������
            }).AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();
            services.AddControllersWithViews();


            //��������� ������� ��� ������������ � ������������� (MVC)
            services.AddControllersWithViews()
                // ������������� � ������� 3.0 ��� ������������ ��������� �������
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //� �������� ���������� ��� ����� ������ ����� ������ ������
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            //���������� ��������� ��������� ������ � ���������� (css, js � �.�.)
            app.UseStaticFiles();

            //���������� ������� �������������
            app.UseRouting();

            //���������� �������������� � �����������
            app.UseCookiePolicy();
            app.UseAuthentication();    
            app.UseAuthorization();

            //������������� ������ ��� �������� (���������)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

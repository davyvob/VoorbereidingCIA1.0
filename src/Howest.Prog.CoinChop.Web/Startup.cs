using Howest.Prog.CoinChop.Core.Interfaces;
using Howest.Prog.CoinChop.Core.Services;
using Howest.Prog.CoinChop.Infrastructure.Data;
using Howest.Prog.CoinChop.Infrastructure.Email;
using Howest.Prog.CoinChop.Infrastructure.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Howest.Prog.CoinChop.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //bind mail configuration
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            //register DbContext
            services.AddDbContext<CoinChopDataContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("CoinChopDb")));

            //register core services
            services.AddTransient<IMemberRepository, MemberRepository>();
            services.AddTransient<IExpenseGroupRepository, ExpenseGroupRepository>();
            services.AddTransient<IExpenseRepository, ExpenseRepository>();
            services.AddTransient<IMailService, SmtpMailService>();
            services.AddTransient<ITokenService, RandomTokenService>();

            //register application services
            services.AddTransient<DebtCalculator>();
            services.AddTransient<ExpenseGroupService>();
            services.AddTransient<ExpenseService>();
            services.AddTransient<MemberService>();


            services.AddControllersWithViews();
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();

            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(
                       name: "newGroup",
                       pattern: "group/add",
                       defaults: new { controller = "ExpenseGroup", action = "Create" });

                routes.MapControllerRoute(
                       name: "expenseRoute",
                       pattern: "group/{groupToken}/expenses/{action}",
                       defaults: new { controller = "Expense" });

                routes.MapControllerRoute(
                       name: "memberRoute",
                       pattern: "group/{groupToken}/members/{action}",
                       defaults: new { controller = "Member" });

                routes.MapControllerRoute(
                       name: "groupDashboard",
                       pattern: "group/{groupToken?}/{action}",
                       defaults: new { controller = "ExpenseGroup", action = "Dashboard" });

                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=ExpenseGroup}/{action=Index}");
            });
        }
    }
}

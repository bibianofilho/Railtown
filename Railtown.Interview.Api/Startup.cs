using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Railtown.Interview.Api.Clients;
using Railtown.Interview.Api.Services;

namespace Railtown.Interview.Api
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
            services.AddControllers();

            services.AddSingleton<IUserService, UserService>();
            services.AddMvc().AddFluentValidation(o => o.ImplicitlyValidateChildProperties = true);

            services.AddHttpClient<IUsersApiClient, UsersApiClient>(
                client =>
                {
                    var usersServerOptions = new UsersServerOptions();
                    Configuration.GetSection("UsersService").Bind(usersServerOptions);
                    client.BaseAddress = usersServerOptions.Url;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

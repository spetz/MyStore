using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyStore.Infrastructure;
using MyStore.Services;
using MyStore.Web.Framework;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace MyStore.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppOptions>(Configuration.GetSection("app"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(o => o.SerializerSettings.Formatting = Formatting.Indented);
            services.AddTransient<ErrorHandlerMiddleware>();
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
//            services.AddDistributedRedisCache(c =>
//            {
//                c.Configuration = "localhost";
//                c.InstanceName = "api:";
//            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                    {
                        Title = "MyStore API",
                        Version = "v1"
                    }
                );
//                c.IncludeXmlComments();
            });
            
            services.AddHealthChecks()
                .AddCheck<RandomHealthCheck>("random");

            services.AddHostedService<UsersProcessorHostedService>();
            services.AddHttpClient<IReqResClient, ReqResClient>(c =>
            {
                c.BaseAddress = new Uri("https://reqres.in/api/");
            });
            
            var builder =  new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<ServicesModule>();
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
            }

//            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseHealthChecks("/health");
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyStore API v1"));
//            app.Use(async (ctx, next) =>
//            {
//                Console.WriteLine("BEFORE");
//                await next();
//            });
//            app.Run(async ctx =>
//            {
//                Console.WriteLine("RUN");
//                await Task.CompletedTask;
//            });

            app.UseMvc();

            lifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}

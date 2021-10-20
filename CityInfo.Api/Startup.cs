using CityInfo.Api.Repositories;
using CityInfo.Api.Repositories.Interfaces;
using CityInfo.Api.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CityInfo.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(options=> {
                options.AddPolicy(Utility.CORE_POLICY, builder => {
                    builder.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            //.AddMvcOptions(o=> {
            //    // this will allow to add xml formater in case if some one add accept header as xml
            //    o.OutputFormatters.Add(
            //        new XmlDataContractSerializerOutputFormatter()
            //});
            //.AddJsonOptions(o => {
            //    // this would remove the json nameing convention.
            //    if(o.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        castedResolver.NamingStrategy = null;
            //    }
            //});

            // configuering services to use for Dependency injection.
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            // you can use three of the following methods to add the dependency depending on requirement.
            // addScoped 
            // addSingleton 
            // addTransiant
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // shows UseCors with any policy builder
            app.UseCors(Utility.CORE_POLICY);

            // status code pages allows to send page response for browsers.
            app.UseStatusCodePages();

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    throw new Exception("Exception Hello World!");
            //});
        }
    }
}

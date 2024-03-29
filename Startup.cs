using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options => options.UseMySQL("Server=localhost;Database=BookStore;Uid=root;Pwd=Mysql@2021;"));
            services.AddControllersWithViews();
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
            // Uncomment this code to disable client side validation.
            // .AddViewOptions(option =>{
            // option.HtmlHelperOptions.ClientValidationEnabled = false
            // )};
#endif
            services.AddScoped<BookRepository, BookRepository>();
            services.AddScoped<LanguageRepository, LanguageRepository>();
            services.AddScoped<BookGalleryRepository, BookGalleryRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            // app.Use(async (context, next)=>{
            //     await context.Response.WriteAsync("Hello From my first middleware \n");
            //     await next();
            //     await context.Response.WriteAsync("Hello From my first middleware Response ");
            // });
            //             app.Use(async (context, next)=>{
            //     await context.Response.WriteAsync("Hello From my second middleware \n");
            //     await next();
            //     await context.Response.WriteAsync("Hello From my second middleware Response ");
            // });
            // app.Use(async (context, next)=>{
            //     await context.Response.WriteAsync("Hello From my third middleware \n");
            // });
            
            app.UseStaticFiles();
            app.UseRouting();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

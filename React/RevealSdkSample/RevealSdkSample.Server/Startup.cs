using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Reveal.Sdk;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RevealSdkSample.Server.RevealSdk;

namespace RevealSdkSample.Server
{
    public class Startup
    {
        private string _webRootPath;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _webRootPath = env.WebRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors();

            var embedSettings = new RevealEmbedSettings();
            embedSettings.LocalFileStoragePath = GetLocalFileStoragePath(_webRootPath);

            var cacheFilePath = Configuration.GetSection("Caching")?["CacheFilePath"] ?? @"Cache";
            Directory.CreateDirectory(cacheFilePath);
            embedSettings.DataCachePath = cacheFilePath;
            embedSettings.CachePath = cacheFilePath;
            services.AddRevealServices(embedSettings, CreateSdkContext());

            services.AddMvc().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(builder => builder
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        protected virtual string GetLocalFileStoragePath(string webRootPath)
        {
            return Path.Combine(webRootPath, "App_Data", "RVLocalFiles");
        }
        protected virtual RevealSdkContextBase CreateSdkContext()
        {
            return new RevealSdkContext();
        }
    }
}

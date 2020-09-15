using AbpBase.Application;
using AbpBase.HttpApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace AbpBase.Web
{
    [DependsOn(
        typeof(AbpBaseApplicationModule),
        typeof(AbpBaseHttpApiModule),
        typeof(AbpAspNetCoreMvcModule)
        )]
    public class AbpBaseWebModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }

        public override void OnApplicationInitialization(
            ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseConfiguredEndpoints();
        }
    }
}

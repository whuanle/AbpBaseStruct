﻿using AbpBase.Application;
using AbpBase.HttpApi;
using AbpBase.Web.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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

        private const string GanweiCosr = "AllowSpecificOrigins";


        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            Configure<MvcOptions>(options =>
            {
                // 全局异常拦截器
                options.Filters.Add(typeof(WebGlobalExceptionFilter));
            });



            // 跨域请求
            ConfigureCors(context);

            // 全局 API 请求实体验证失败信息格式化
            context.Services.GlabalInvalidModelStateFilter();
        }

        /// <summary>
        /// 配置跨域
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureCors(ServiceConfigurationContext context)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(GanweiCosr,
                    builder => builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin());
            });
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

            app.UseCors(GanweiCosr);

            app.UseConfiguredEndpoints();
        }
    }
}

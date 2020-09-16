using AbpBase.Application.Contracts;
using AbpBase.Database;
using AbpBase.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace AbpBase.Application
{
    [DependsOn(
        typeof(AbpBaseDomainModule),
        typeof(AbpBaseApplicationContractsModule),
        typeof(AbpBaseDatabaseModule)
    )]
    public class AbpBaseApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<Serilog.ILogger>(Serilog.Log.Logger);
        }
    }
}

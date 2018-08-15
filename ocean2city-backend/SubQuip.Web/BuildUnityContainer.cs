using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ocean2City.Data.Interfaces;
using Ocean2City.Data.Logic;
using Ocean2City.Business.Interfaces;
using Ocean2City.Business.Logic;

namespace Ocean2City.WebApi
{
    /// <summary>
    /// Container
    /// </summary>
    public static class BuildUnityContainer
    {
        /// <summary>
        /// Register Service and Repository
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAddTransient(IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            #region Repository
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            #endregion

            #region Services
            services.AddTransient<ICategoryManagerService, CategoryManagerService>();
            services.AddTransient<IItemManagerService, ItemManagerService>();
            #endregion

            return services;
        }
    }
}

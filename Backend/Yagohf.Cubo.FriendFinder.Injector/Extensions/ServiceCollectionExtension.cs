using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Yagohf.Cubo.FriendFinder.Injector.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInjectorBootstrapper(this IServiceCollection services, IConfiguration configuration)
        {
            InjectorBootstrapper.RegisterServices(services, configuration);
        }
    }
}

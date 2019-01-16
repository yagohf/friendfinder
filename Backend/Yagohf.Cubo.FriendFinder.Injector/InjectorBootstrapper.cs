using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yagohf.Cubo.FriendFinder.Business.Domain;
using Yagohf.Cubo.FriendFinder.Business.Helper;
using Yagohf.Cubo.FriendFinder.Business.Interface.Domain;
using Yagohf.Cubo.FriendFinder.Business.Interface.Helper;
using Yagohf.Cubo.FriendFinder.Business.MapperProfile;
using Yagohf.Cubo.FriendFinder.Data.Context;
using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Data.Interface.Repository;
using Yagohf.Cubo.FriendFinder.Data.Query;
using Yagohf.Cubo.FriendFinder.Data.Repository;

namespace Yagohf.Cubo.FriendFinder.Injector
{
    public static class InjectorBootstrapper
    {
        private const string CONNECTION_STRING_FRIENDFINDERDB = "FriendFinderDB";

        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            //Context EF Core
            services.AddDbContext<FriendFinderContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString(CONNECTION_STRING_FRIENDFINDERDB));
            });

            //Data - Repository
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAmigoRepository, AmigoRepository>();
            services.AddScoped<ICalculoHistoricoLogRepository, CalculoHistoricoLogRepository>();

            //Data - Query
            services.AddScoped<IUsuarioQuery, UsuarioQuery>();
            services.AddScoped<IAmigoQuery, AmigoQuery>();

            //Business - Helper
            services.AddScoped<ITokenHelper, TokenHelper>();
            services.AddScoped<ICalculoHistoricoLogHelper, CalculoHistoricoLogHelper>();

            //Business - Domain
            services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
            services.AddScoped<IAmigoBusiness, AmigoBusiness>();
            services.AddScoped<ICalculadoraDistanciaPontosBusiness, CalculadoraDistanciaPontosBusiness>();

            //Automapper
            MapperConfiguration mapperConfiguration = new MapperConfiguration(mConfig =>
            {
                mConfig.AddProfile(new BusinessMapperProfile());
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}

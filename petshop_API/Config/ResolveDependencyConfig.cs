using Domain.Infraestructure.Notification;
using Domain.Repository;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Data;

namespace petshop_API.Config
{
    public static class ResolveDependencyConfig
    {
        public static void ResolveDependency(this IServiceCollection services)
        {
            services.AddScoped<IDbConnection, MySqlConnection>();
            services.AddScoped<INotification, Notification>();

            services.AddScoped<AlojamentoRepository>();
            services.AddScoped<ClienteRepository>();
            services.AddScoped<AnimalRepository>();
            services.AddScoped<ProntuarioRepository>();
        }
    }                                              
}                                                  
                                                   
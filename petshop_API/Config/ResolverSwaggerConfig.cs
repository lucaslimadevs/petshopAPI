using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace petshop_API.Config
{
    public static class ResolverSwaggerConfig
    {
        public static void ResolveSwagger(this IServiceCollection services) 
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "petshop-api",
                    Description = "Aplicação para controle de alojamentos de um pet shop.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Lucas Santos",
                        Email = "lucaslima.devs@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/lucas-santos-gonçalves-lima-a05a95203")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });
        }
    }
}

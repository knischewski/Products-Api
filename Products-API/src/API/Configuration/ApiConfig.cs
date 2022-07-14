using Microsoft.AspNetCore.Mvc;

namespace API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(this IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder =>
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader());
                               //.AllowCredentials());

                options.AddPolicy("Production",
                    builder =>
                        builder
                            .WithMethods("GET")
                            .WithOrigins("http://seusite.com.br")
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            //.WithHeaders(HeaderNames.ContentType, "x-custom-header")
                            .AllowAnyHeader());
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static IApplicationBuilder UseMvcConfig(this IApplicationBuilder app)
        {
            return app;
        }
    }
}

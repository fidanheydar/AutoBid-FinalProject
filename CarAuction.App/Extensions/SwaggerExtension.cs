using Microsoft.OpenApi.Models;

namespace CarAuction.App.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("admin_v1", new OpenApiInfo
                {
                    Title = "Admin API",
                    Version = "admin_v1",
                    Description = "An API to perform Admin operations",
                });
                c.SwaggerDoc("user_v1", new OpenApiInfo
                {
                    Title = "User API",
                    Version = "user_v1",
                    Description = "An API to perform User operations",
                    TermsOfService = new Uri("https://example.com/terms"),
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

            });
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeNerds.Commons;
using WeNerds.Commons.Extensions;
using WeNerds.Models.Dto;
using WeNerds.Services.Implementation;
using WeNerds.Services.Interfaces;

namespace WeNerds.Services.Configurations
{
    public static class WeJwtConfiguration
    {
        public static void AddWeJwt(this IServiceCollection services, IConfiguration configuration, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            try
            {
                var tokenConfigurations = new WeToken(configuration.GetSection(WeConstants.EV_NAME_JWT_SECRETS).Value.GetValueOrDefault("70ee751e-ab08-47a0-a02a-e667a7e75ffd"),
                                                      configuration.GetSection(WeConstants.EV_NAME_JWT_TOKEN_EXPIRATION).Value.GetValueOrDefault("1").ToUInt(),
                                                      configuration.GetSection(WeConstants.EV_NAME_JWT_REFRESH_TOKEN_EXPIRATION).Value.GetValueOrDefault("48").ToUInt(),
                                                      configuration.GetSection(WeConstants.EV_NAME_JWT_ISSUER).Value.GetValueOrDefault("WeJwt"),
                                                      configuration.GetSection(WeConstants.EV_NAME_JWT_AUDIANCE).Value.GetValueOrDefault("http://localhost"));



                Register(services, tokenConfigurations, serviceLifetime);

                var key = Encoding.ASCII.GetBytes(tokenConfigurations.Secret);

                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {                    
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidAudience = tokenConfigurations.Audience,
                        ValidIssuer = tokenConfigurations.Issuer,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            }
            catch
            {
                throw;
            }
        }

        private static void Register(IServiceCollection services, WeToken tokenConfigurations, ServiceLifetime serviceLifetime)
        {
            try
            {
                services.AddSingleton<WeToken>(tokenConfigurations);
                switch (serviceLifetime)
                {
                    case ServiceLifetime.Transient:
                        services.AddTransient<IWeJwtService, WeJwtService>();
                        
                        break;

                    case ServiceLifetime.Singleton:
                        services.AddSingleton<IWeJwtService, WeJwtService>();                        
                        break;

                    default:
                        services.AddScoped<IWeJwtService, WeJwtService>();
                        break;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

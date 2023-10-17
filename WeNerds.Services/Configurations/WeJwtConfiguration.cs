using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeNerds.Commons.Enumarators;
using WeNerds.Commons.Extensions;
using WeNerds.Models.Dto;
using WeNerds.Services.Implementation;
using WeNerds.Services.Interfaces;

namespace WeNerds.Services.Configurations
{
    public static class WeJwtConfiguration
    {
        public static void AddWeJwt(this IServiceCollection services, IConfiguration configuration, InjectionTypeEnum injectionType = InjectionTypeEnum.Singleton)
        {

            try
            {
                var tokenConfigurations = new WeToken(configuration.GetSection("WeNerds:Jwt:Secret").Value.GetValueOrDefault("70ee751e-ab08-47a0-a02a-e667a7e75ffd"),
                                                      configuration.GetSection("WeNerds:Jwt:TokenExpiresInHours").Value.GetValueOrDefault("1").ToUInt(),
                                                      configuration.GetSection("WeNerds:Jwt:RefreshTokenExpiresInHours").Value.GetValueOrDefault("48").ToUInt(),
                                                      configuration.GetSection("WeNerds:Jwt:Issuer").Value.GetValueOrDefault("WeJwt"),
                                                      configuration.GetSection("WeNerds:Jwt:Audience").Value.GetValueOrDefault("http://localhost"));



                Register(services, tokenConfigurations, injectionType);

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

        private static void Register(IServiceCollection services, WeToken tokenConfigurations, InjectionTypeEnum injectionType)
        {
            try
            {
                services.AddSingleton<WeToken>(tokenConfigurations);
                switch (injectionType)
                {
                    case InjectionTypeEnum.Transient:
                        services.AddSingleton<IWeJwtService, WeJwtService>();
                        break;

                    case InjectionTypeEnum.Scoped:
                        services.AddScoped<IWeJwtService, WeJwtService>();
                        break;

                    default:
                        services.AddSingleton<IWeJwtService, WeJwtService>();
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

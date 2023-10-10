using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GRpcProtocolGenerator.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sample.Gateway
{
    /// <summary>
    /// swagger
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// 注册 swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, SwaggerConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc(config.Version, new OpenApiInfo { Title = config.Title, Version = config.Version });

                switch (config.SwaggerConfigType)
                {
                    case SwaggerConfigType.None:
                        break;
                    case SwaggerConfigType.Bearer:
                        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                        {
                            Description = "Jwt Bearer Token",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            BearerFormat = "JWT",
                            Scheme = "Bearer"
                        });

                        option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                                new string[] { }
                            }
                        });
                        break;
                    case SwaggerConfigType.IdentityLogin:
                        option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.OAuth2,
                            Flows = new OpenApiOAuthFlows
                            {
                                Implicit = new OpenApiOAuthFlow
                                {
                                    AuthorizationUrl = new Uri($"{config.IdentityUrl}/connect/authorize"),
                                    TokenUrl = new Uri($"{config.IdentityUrl}/connect/token"),
                                    Scopes = config.GetScopes().ToDictionary(d => d, d => d)
                                },
                            }
                        });

                        var oAuthScheme = new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        };

                        option.AddSecurityRequirement(new()
                        {
                            [oAuthScheme] = new List<string>()
                        });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                foreach (var xml in config.DocumentXml)
                {
                    AddXml(option, xml);
                }
            });

            //void AddXml(SwaggerGenOptions c, string name)
            //{
            //    var filePath = Path.Combine(AppContext.BaseDirectory, name);
            //    c.IncludeXmlComments(filePath);
            //}

            void AddXml(SwaggerGenOptions c, string name)
            {
                c.DocInclusionPredicate((_, api) => string.IsNullOrWhiteSpace(api.GroupName) == false);
                c.SwaggerGeneratorOptions.TagsSelector = (api) => new[] { api.GroupName };

                var filePath = Path.Combine(AppContext.BaseDirectory, name);
                c.IncludeXmlComments(filePath);
                //c.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
            }

            return services;
        }

        /// <summary>
        /// 注册 swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static WebApplication UseCustomSwagger(this WebApplication app, SwaggerConfig config)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{config.Version}/swagger.json", config.Title);

                if (config.SwaggerConfigType != SwaggerConfigType.IdentityLogin)
                    return;

                c.OAuthClientId(config.ClientId);
                c.OAuthAppName(config.Name);
                c.OAuthScopes(config.GetScopes().ToArray());
                c.OAuthClientSecret(config.ClientSecret);
            });

            return app;
        }
    }
}
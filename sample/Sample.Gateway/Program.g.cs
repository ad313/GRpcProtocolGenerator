﻿/*
using GRpcProtocolGenerator.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Sample.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var config = new SwaggerConfig()
            {
                SwaggerConfigType = SwaggerConfigType.IdentityLogin,
                Name = "GRpc Client + Restful api",
                Title = "GRpc Client + Restful api",
                Audience = "attendancesystem",
                Scope = new string[]
                {
                    "gateway",
                },
                ClientId = "64057d47d3b24a0001470082",
                ClientSecret = "secret",
                IdentityUrl = "https://192.168.1.20:8443",
                Version = "v1",
                DocumentXml = new string[]
                {
                    "Sample.Gateway.xml",
                    "Sample.Protocol.xml",
                }
            };

            //swagger
            builder.Services.AddCustomSwagger(config);

            builder.Services.AddCors(option => option.AddPolicy("cors", policy =>
               policy.AllowAnyHeader()
                   .AllowAnyMethod()
                   .WithExposedHeaders("Content-Disposition")
                   .AllowAnyOrigin()));

            //认证授权
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim("sub");
                });
            });

            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.Audience = config.Audience;
                options.Authority = config.IdentityUrl;
                options.RequireHttpsMetadata = false;
                options.BackchannelHttpHandler = new HttpClientHandler() { ServerCertificateCustomValidationCallback = ((_, _, _, _) => true) };
            });

            //注册 gRpc 客户端
            builder.Services.RegisterClient(builder.Configuration.GetSection("client_address_key").Get<string>());

            var app = builder.Build();

            //swagger
            app.UseCustomSwagger(config);

            app.UseRouting();
            app.UseCors("cors");

            app.UseAuthentication();
            app.UseAuthorization();       

            app.MapControllers();

            app.Run();
                   
            //配置文件
            //"client_address_key": "https://localhost:6010",
            //"Kestrel": {
            //  "Endpoints": {
            //    "http": {
            //      "Url": "http://*:6011"
            //    }
            //  }
            //}            
        }
    }    
}
*/
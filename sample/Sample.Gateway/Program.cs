using GRpcProtocolGenerator.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace Sample.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var config = new SwaggerConfig()
            {
                SwaggerConfigType = SwaggerConfigType.IdentityLogin,
                Name = "GRpc Server + Restful api",
                Title = "gRPC transcoding",
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

            builder.Services.AddCustomSwagger(config);

            builder.Services.AddCors(option => option.AddPolicy("cors", policy =>
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("Content-Disposition")
                    .AllowAnyOrigin()));

            //ÈÏÖ¤ÊÚÈ¨
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

            builder.Services.RegisterClient("https://localhost:50420");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //swagger
                app.UseCustomSwagger(config);
            }

            app.UseRouting();
            app.UseCors("cors");

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}
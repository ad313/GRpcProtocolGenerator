/*
using Grpc.Core;
using Grpc.Core.Interceptors;
using GRpcProtocolGenerator.Common;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Sample.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            var builder = WebApplication.CreateBuilder(args);

            var config = new SwaggerConfig()
            {
                SwaggerConfigType = SwaggerConfigType.None,
                Name = "",
                Title = "",
                Audience = "",
                Scope = new string[]
                {
                },
                ClientId = "",
                ClientSecret = "",
                IdentityUrl = "",
                Version = "",
                DocumentXml = new string[]
                {
                }
            };
            
            builder.Services.AddGrpc();         
            

            var app = builder.Build();    
                        
            
            //注册 gRpc Server
            app.MapGrpcService<Sample.Server.Implements.GRpcServiceTestImpl>();
            app.MapGrpcService<Sample.Server.Implements.GRpcServiceTest2ServiceImpl>();

            app.Run();
        }
    }
    
}
*/
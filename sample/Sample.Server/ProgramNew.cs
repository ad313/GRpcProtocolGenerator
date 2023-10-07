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
                    "Sample.Server.xml",
                    "Sample.Protocol.xml",
                }
            };
            
            builder.Services.AddGrpc(option =>
                {
                    option.Interceptors.Add<ErrorInterceptor>();
                })
                .AddJsonTranscoding(o =>
                {
                    o.JsonSettings.WriteIndented = true;
                    o.JsonSettings.WriteEnumsAsIntegers = true;
                });            

            //swagger
            builder.Services.AddGrpcSwagger();
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

            var app = builder.Build();     
            
            //swagger
            app.UseCustomSwagger(config);

            app.UseRouting();
            app.UseCors("cors");    
            
            app.UseAuthentication();
            app.UseAuthorization();            
            
            //注册 gRpc Server
            app.MapGrpcService<Sample.Server.Implements.GRpcServiceTestImpl>();

            app.Run();
        }
    }
        
    /// <summary>
    /// 拦截器，处理GRpc异常
    /// </summary>
    public class ErrorInterceptor : Interceptor
    {
        private readonly ILogger<ErrorInterceptor> _logger;

        public ErrorInterceptor(ILogger<ErrorInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            LogCall<TRequest, TResponse>(MethodType.Unary, context);
            
            try
            {
                return await continuation(request, context);
            }
            catch (RpcException e)
            {
                throw new RpcException(new Status(StatusCode.Unknown, e.Message), e.Message);
            }
            catch (Exception e)
            {
                throw new RpcException(new Status(StatusCode.Unknown, e.Message), e.Message);
            }
        }

        private void LogCall<TRequest, TResponse>(MethodType methodType, ServerCallContext context)
            where TRequest : class
            where TResponse : class
        {
            _logger.LogWarning($"Starting call. Type: {methodType}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}");
        }
    }
}
*/
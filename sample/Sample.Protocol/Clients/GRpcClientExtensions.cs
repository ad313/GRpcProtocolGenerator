/*
	 此代码是自动生成，请勿修改，如果要修改，请继承它再重写方法。
*/

using System;
using Sample.Protocol.Clients;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GRpcClientExtensions
    {
        /// <summary>
        /// 注册客户端
        /// </summary>
        /// <param name="services"></param>
        /// <param name="address">gRpc server 地址</param>
        /// <returns></returns>
        public static IServiceCollection RegisterClient(this IServiceCollection services, string address)
        {
            ArgumentNullException.ThrowIfNull(address,nameof(address));

            //GRpc 客户端服务提供者
            services.AddSingleton<IGRpcClientProvider>(new GRpcClientProvider(address));

            return services.RegisterClientService();
        }

        /// <summary>
        /// 注册客户端，自定义 GRpc 客户端服务提供者，继承 GRpcClientProvider 或 实现 IGRpcClientProvider接口
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterClient<TGRpcClientProvider>(this IServiceCollection services) where TGRpcClientProvider : class, IGRpcClientProvider
        {
            //GRpc 客户端服务提供者
            services.AddSingleton<IGRpcClientProvider, TGRpcClientProvider>();

            return services.RegisterClientService();
        }

        // GRpc 代理服务
        private static IServiceCollection RegisterClientService(this IServiceCollection services)
        {
            services.AddScoped<IGRpcServiceTestClient, GRpcServiceTestClient>();
            services.AddScoped<IGRpcServiceTest2ServiceClient, GRpcServiceTest2ServiceClient>();            

            return services;
        }
    }
}
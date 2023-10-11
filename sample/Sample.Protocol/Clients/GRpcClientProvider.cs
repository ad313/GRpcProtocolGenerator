/*
	 此代码是自动生成，请勿修改，如果要修改，请继承它再重写方法。
*/

using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Concurrent;
using System.Reflection.Emit;

namespace Sample.Protocol.Clients
{
    /// <summary>
    /// GRpc客户端服务提供者
    /// </summary>
    public interface IGRpcClientProvider
    {
        /// <summary>
        /// 创建GRpc客户端
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CreateClient<T>() where T : ClientBase<T>;
    }

    /// <summary>
    /// GRpc客户端服务提供者
    /// </summary>
    public class GRpcClientProvider : IGRpcClientProvider
    {
        /// <summary>
        /// 远程gRpc 地址
        /// </summary>
        protected readonly string GRpcAddress;

        /// <summary>
        /// gRpcChannel
        /// </summary>
        protected Lazy<GrpcChannel> Channel;

        /// <summary>
        /// 缓存客户端创建委托
        /// </summary>
        protected ConcurrentDictionary<Type, Delegate> ClientDelegateDictionary = new ConcurrentDictionary<Type, Delegate>();

        /// <summary>
        /// 初始化
        /// </summary>
        public GRpcClientProvider() { }

        /// <summary>
        /// 带地址初始化
        /// </summary>
        public GRpcClientProvider(string gRpcAddress)
        {
            GRpcAddress = gRpcAddress;
            Channel = new Lazy<GrpcChannel>(CreateGRpcChannel);
        }

        /// <summary>
        /// 创建GRpc客户端
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T CreateClient<T>() where T : ClientBase<T>
        {
            return GetCreateClientFunc<ChannelBase, T>()(Channel.Value);
        }

        /// <summary>
        /// 创建GRpc Channel
        /// </summary>
        /// <returns></returns>
        public virtual GrpcChannel CreateGRpcChannel()
        {
            return GrpcChannel.ForAddress(GRpcAddress);
        }

        private Func<TIn, TOut> GetCreateClientFunc<TIn, TOut>()
        {
            return ClientDelegateDictionary.GetOrAdd(typeof(TOut), type => CreateDelegate<TIn, TOut>()) as Func<TIn, TOut>;
        }

        /// <summary>
        /// 创建 client 对应的委托
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <returns></returns>
        private Delegate CreateDelegate<TIn, TOut>()
        {
            var dynamic = new DynamicMethod("DynamicMethod", typeof(TOut), new[] { typeof(TIn) }, typeof(GRpcClientProvider).Module, false);
            var ilGenerator = dynamic.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Newobj, typeof(TOut).GetConstructor(new Type[] { typeof(TIn) }));
            ilGenerator.Emit(OpCodes.Ret);
            return dynamic.CreateDelegate(typeof(Func<TIn, TOut>));
        }
    }
}
﻿using Google.Protobuf;
using Mapster;

namespace Sample.Server.Mapper
{
    //public class MapperRegister : IRegister
    //{
    //    /// <summary>
    //    /// Register
    //    /// </summary>
    //    /// <param name="config"></param>
    //    public void Register(TypeAdapterConfig config)
    //    {
    //        new GRpcMapperRegister().Register(config);
    //    }
    //}

    /// <summary>
    /// Mapster 默认配置
    /// </summary>
    public class GRpcMapperRegister
    {
        /// <summary>
        /// 默认转换配置
        /// </summary>
        /// <param name="config"></param>
        public void Register(TypeAdapterConfig config)
        {
            config.Default.PreserveReference(true);
            config.Default.MaxDepth(5);
            config.Default.UseDestinationValue(member => member.SetterModifier == AccessModifier.None && member.Type.IsGenericType);
            config.Default.UseDestinationValue(member => member.AccessModifier == AccessModifier.None);

            //gRpc ByteString byte[] 相互转换
            TypeAdapterConfig<byte[], ByteString>.NewConfig().MapWith(bytes => bytes == null ? null : UnsafeByteOperations.UnsafeWrap(bytes));
            TypeAdapterConfig<ByteString, byte[]>.NewConfig().MapWith(str => str.IsEmpty ? null : str.ToByteArray());
        }
    }
}
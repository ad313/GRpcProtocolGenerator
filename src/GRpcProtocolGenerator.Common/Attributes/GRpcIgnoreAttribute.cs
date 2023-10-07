using System;

namespace GRpcProtocolGenerator.Common.Attributes
{
    /// <summary>
    /// 忽略 gRpc 生成；忽略方法和属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class GRpcIgnoreAttribute : Attribute
    {
    }
}
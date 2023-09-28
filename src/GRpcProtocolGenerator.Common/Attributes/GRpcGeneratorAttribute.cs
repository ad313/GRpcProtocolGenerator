using System;

namespace GRpcProtocolGenerator.Common.Attributes
{
    /// <summary>
    /// 标记需要生成 gRpc 的接口
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class GRpcGeneratorAttribute : Attribute
    {
    }
}
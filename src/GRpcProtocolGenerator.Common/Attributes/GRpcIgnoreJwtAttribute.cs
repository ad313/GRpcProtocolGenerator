using System;

namespace GRpcProtocolGenerator.Common.Attributes
{
    /// <summary>
    /// 忽略 jwt 验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Interface)]
    public class GRpcIgnoreJwtAttribute : Attribute
    {
    }
}
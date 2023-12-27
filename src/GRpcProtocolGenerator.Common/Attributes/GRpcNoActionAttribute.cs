using System;

namespace GRpcProtocolGenerator.Common.Attributes
{
    /// <summary>
    /// 标记 不生成控制器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GRpcNoActionAttribute : Attribute
    {
    }
}
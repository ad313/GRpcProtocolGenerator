using System.Reflection;

namespace GRpcProtocolGenerator.Models.MetaData;

/// <summary>
/// 不支持的方法及其原因
/// </summary>
/// <param name="MethodInfo">方法信息</param>
/// <param name="Reason">不支持的原因</param>
public sealed record NotSupportedMethodInfo(MethodInfo MethodInfo, string Reason);

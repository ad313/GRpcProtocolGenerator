using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using GRpcProtocolGenerator.Renders;
using GRpcProtocolGenerator.Types;

namespace GRpcProtocolGenerator.Models.MetaData;

/// <summary>
/// 方法元数据
/// </summary>
public class MethodMetaData : CommentMetaData
{
    public int Index { get; set; }

    public InterfaceMetaData? InterfaceMetaData { get; private set; }

    public MethodInfo MethodInfo { get; }

    public string? Key { get; private set; }

    /// <summary>
    /// 标记 传入参数是否有 CancellationToken 参数
    /// </summary>
    public bool HasCancellationToken { get; private set; }

    /// <summary>
    /// 传入参数集合
    /// </summary>
    public List<PropertyMetaData> InParamMetaDataList { get; private set; }

    /// <summary>
    /// 传出参数集合
    /// </summary>
    public List<PropertyMetaData> OutParamMetaDataList { get; private set; }

    public bool IsTask { get; private set; }

    /// <summary>
    /// gRPC 流式类型
    /// </summary>
    public StreamKind StreamKind { get; private set; }

    /// <summary>
    /// 流式请求元素元数据（客户端流/双向流时有效，用于生成请求消息）
    /// </summary>
    public PropertyMetaData? StreamRequestElement { get; private set; }

    /// <summary>
    /// 流式响应元素元数据（服务端流/双向流时有效，用于生成响应消息）
    /// </summary>
    public PropertyMetaData? StreamResponseElement { get; private set; }

    /// <summary>
    /// 是否服务端流（含双向流）
    /// </summary>
    public bool IsServerStreaming => StreamKind is StreamKind.ServerStreaming or StreamKind.DuplexStreaming;

    /// <summary>
    /// 是否客户端流（含双向流）
    /// </summary>
    public bool IsClientStreaming => StreamKind is StreamKind.ClientStreaming or StreamKind.DuplexStreaming;

    public string? GrpcMethodName => this.FormatServiceMethodName();

    public MethodMetaData(string? @namespace, string? name, MethodInfo methodInfo, List<AttributeMetaData> attributeMetaDataList, List<PropertyMetaData> inParamMetaDataList, List<PropertyMetaData> outParamMetaDataList,
        StreamKind streamKind = StreamKind.None, PropertyMetaData? streamRequestElement = null, PropertyMetaData? streamResponseElement = null)
        : base(@namespace, name, attributeMetaDataList)
    {
        MethodInfo = methodInfo;
        InParamMetaDataList = inParamMetaDataList ?? [];
        OutParamMetaDataList = outParamMetaDataList ?? [];
        IsTask = methodInfo.ReturnType.IsTask();
        StreamKind = streamKind;
        StreamRequestElement = streamRequestElement;
        StreamResponseElement = streamResponseElement;

        //标记 传入参数是否有 CancellationToken 参数
        HasCancellationToken = InParamMetaDataList.Exists(d => d.TypeWrapper.Type == typeof(CancellationToken));

        SetKey();
    }

    /// <summary>
    /// 过滤得到有效参数
    /// </summary>
    public List<PropertyMetaData> InParamMetaDataListFilter() => InParamMetaDataList.Where(d => d.TypeWrapper.Type != typeof(CancellationToken)).ToList();

    private void SetKey()
    {
        Key = !InParamMetaDataList.Any() ? FullName : $"{FullName}_{string.Join("_", InParamMetaDataList.Select(d => d?.TypeWrapper.IsArray + "_" + d?.TypeWrapper.IsNullable + "_" + d?.FullName))}";
    }

    public void SetInterfaceMetaData(InterfaceMetaData interfaceMetaData)
    {
        InterfaceMetaData = interfaceMetaData;
    }

    public string GetKey()
    {
        return $"{string.Join(".", InParamMetaDataList.Select(d => d.FullName))}_" +
               Name +
               $"{string.Join(".", OutParamMetaDataList.Select(d => d.FullName))}";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GRpcProtocolGenerator.Common.Attributes;
using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Models.MetaData;

namespace GRpcProtocolGenerator.Renders.Protocol;

public class ProtocolService
{
    /// <summary>
    /// Service名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 方法集合
    /// </summary>
    public List<ProtoServiceItem> Items { get; set; }

    /// <summary>
    /// 接口
    /// </summary>
    public InterfaceMetaData InterfaceMetaData { get; set; }

    public ProtocolService(string name, List<ProtoServiceItem> items, InterfaceMetaData interfaceMetaData)
    {
        Name = name;
        Items = items.OrderBy(d => d.Index).ToList();
        InterfaceMetaData = interfaceMetaData;
    }

    /// <summary>Returns a string that represents the current object.</summary>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"//{Config.ConfigInstance.Proto.GetPropertyDescription(InterfaceMetaData)} {InterfaceMetaData.FullName}");
        sb.AppendLine($"service {InterfaceMetaData.FormatServiceName()} " + "{");

        foreach (var item in Items)
        {
            sb.AppendLine("\t" + item.ToString());
        }
        sb.AppendLine("}");
        return sb.ToString();
    }
}

public class ProtoServiceItem
{
    public int Index { get; set; }

    /// <summary>
    /// 方法名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 传入参数
    /// </summary>
    public ProtocolMessage? InParam { get; set; }

    /// <summary>
    /// 传出参数
    /// </summary>
    public ProtocolMessage? OutParam { get; set; }

    public MethodMetaData MethodMetaData { get; set; } = null!;

    public ProtocolHttpOption? HttpOption { get; set; }

    /// <summary>
    /// gRPC 流式类型
    /// </summary>
    public StreamKind StreamKind => MethodMetaData.StreamKind;

    /// <summary>
    /// 是否服务端流（含双向流）
    /// </summary>
    public bool IsServerStreaming => MethodMetaData.IsServerStreaming;

    /// <summary>
    /// 是否客户端流（含双向流）
    /// </summary>
    public bool IsClientStreaming => MethodMetaData.IsClientStreaming;

    /// <summary>Returns a string that represents the current object.</summary>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine();
        sb.AppendLine($"\t// {Config.ConfigInstance.Proto.GetPropertyDescription(MethodMetaData)}");

        //流式与 gRPC JSON 转码不兼容，不生成 http option（控制器也不会包含流式方法）
        if (StreamKind == StreamKind.None)
        {
            HttpOption = GetHttpOption(HttpMethod.Get, typeof(HttpGetAttribute)) ??
                         GetHttpOption(HttpMethod.Post, typeof(HttpPostAttribute)) ??
                         GetHttpOption(HttpMethod.Put, typeof(HttpPutAttribute)) ??
                         GetHttpOption(HttpMethod.Delete, typeof(HttpDeleteAttribute)) ??
                         GetHttpOption(HttpMethod.Patch, typeof(HttpPatchAttribute));
        }

        var inStream = IsClientStreaming ? "stream " : "";
        var outStream = IsServerStreaming ? "stream " : "";

        if (Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding)
        {
            sb.Append($"\trpc {Name}({inStream}{InParam?.GetGRpcName()}) returns({outStream}{OutParam?.GetGRpcName()})");

            //没标记 http，不生成
            if (HttpOption != null)
            {
                sb.Append(HttpOption.ToString());
            }
            else if (IsServerStreaming || IsClientStreaming)
            {
                //流式方法没有 http option，必须以分号结尾才是合法 proto
                sb.Append(";");
            }
        }
        else
        {
            sb.Append($"\trpc {Name}({inStream}{InParam?.GetGRpcName()}) returns({outStream}{OutParam?.GetGRpcName()});");
        }

        return sb.ToString();
    }

    private ProtocolHttpOption? GetHttpOption(HttpMethod httpMethod, Type type)
    {
        var httpAttribute = MethodMetaData.AttributeMetaDataList.FirstOrDefault(d => d.Type == type);
        if (httpAttribute != null)
        {
            //获取路由
            var route = httpAttribute.ConstructorDictionary?.FirstOrDefault().Value ??
                        httpAttribute.NamedDictionary?.FirstOrDefault().Value ?? "";

            return new ProtocolHttpOption(httpMethod, route?.ToString() ?? "", MethodMetaData.InterfaceMetaData!);
        }

        return null;
    }
}

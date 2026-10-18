using System;
using System.ComponentModel;
using System.Text;
using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Types;

namespace GRpcProtocolGenerator.Renders.Protocol;

/// <summary>
/// 生成 Http option
/// </summary>
public class ProtocolHttpOption(HttpMethod httpMethod, string route, InterfaceMetaData interfaceMetaData)
{
    public HttpMethod HttpMethod { get; set; } = httpMethod;

    public string Route { get; set; } = route;

    public InterfaceMetaData InterfaceMetaData { get; } = interfaceMetaData;

    /// <summary>Returns a string that represents the current object.</summary>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(" {");
        sb.AppendLine("\t\toption (google.api.http) = {");
        sb.AppendLine($"\t\t\t{HttpMethod.GetDescription()}:{GetRoute()}");

        switch (HttpMethod)
        {
            case HttpMethod.Get:
                break;
            case HttpMethod.Post:
            case HttpMethod.Put:
                sb.AppendLine("\t\t\tbody:\"*\"");
                break;
            case HttpMethod.Delete:
                break;
            case HttpMethod.Patch:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        sb.AppendLine("\t\t};");
        sb.Append("\t}");

        return sb.ToString();
    }

    private string GetRoute()
    {
        var route = $"{InterfaceMetaData.FormatServiceName()}" + $"{(string.IsNullOrWhiteSpace(Route) ? "" : "/" + Route)}";

        var newRoute = Config.ConfigInstance.JsonTranscoding.RouteFunc?.Invoke(route);
        return string.IsNullOrWhiteSpace(newRoute) ? $"\"/api/v1/{route}" + "\"," : $"\"/{newRoute}" + "\",";
    }
}

public enum HttpMethod
{
    [Description("get")]
    Get,
    [Description("post")]
    Post,
    [Description("put")]
    Put,
    [Description("delete")]
    Delete,
    [Description("patch")]
    Patch
}

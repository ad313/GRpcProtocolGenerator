using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Renders.Protocol;
using System;
using System.Text;
using System.Xml.Linq;

namespace GRpcProtocolGenerator.Renders
{
    public static partial class BuilderName
    {
        //public static string FormatGRpcServerName(this InterfaceMetaData meta)
        //{
        //    ArgumentNullException.ThrowIfNull(meta, nameof(meta));

        //    return Config.ConfigInstance?.Proto?.ServiceNameFunc?.Invoke(meta) ?? meta.Name;
        //}

        //public static string FormatServiceProtoFileName(this InterfaceMetaData meta)
        //{
        //    return meta.FormatServiceName().ToSnakeString();
        //}

        //public static string FormatServiceProtoFileNameFullPath(this InterfaceMetaData meta)
        //{
        //    return Config.ConfigInstance.Proto.UseProtoDirectoryWhenImportPackage ?
        //        $"{Config.ConfigInstance.Proto.ProtoDirectory}/{meta.FormatServiceName().ToSnakeString()}" :
        //        $"{meta.FormatServiceName().ToSnakeString()}";
        //}

        //public static string FormatServiceMethodName(this MethodMetaData method)
        //{
        //    ArgumentNullException.ThrowIfNull(method, nameof(method));

        //    return Config.ConfigInstance?.Proto?.MethodNameFunc?.Invoke(method) ?? method.Name;
        //}

        //public static string FormatMessageName(this ProtocolMessage meta)
        //{
        //    ArgumentNullException.ThrowIfNull(meta, nameof(meta));

        //    if (!meta.IsOriginalClass && meta.EnumMetaData == null)
        //        return meta.Name;

        //    return meta.Name.FormatMessageName();
        //}

        //public static string FormatMessageName(this string name)
        //{
        //    ArgumentNullException.ThrowIfNull(name, nameof(name));

        //    return Config.ConfigInstance?.Proto?.OriginalClassNameFunc?.Invoke(name) ?? name;
        //}

        //public static string FormatGRpcClientName(this string name)
        //{
        //    return $"GRpc{name.TrimStart('I')}Client";
        //}

    }
}

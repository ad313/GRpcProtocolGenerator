using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Renders.Protocol;
using GRpcProtocolGenerator.Types;
using System;
using System.Text;

namespace GRpcProtocolGenerator.Renders
{
    public static class BuilderName
    {
        public static Config Config = null;

        public static string FormatServiceName(this InterfaceMetaData meta)
        {
            ArgumentNullException.ThrowIfNull(meta, nameof(meta));

            return Config?.Proto?.ServiceNameFunc?.Invoke(meta) ?? meta.Name;
        }

        public static string FormatServiceProtoFileName(this InterfaceMetaData meta)
        {
            return meta.FormatServiceName().ToSnakeString();
        }

        public static string FormatServiceProtoFileNameFullPath(this InterfaceMetaData meta)
        {
            return Config.Proto.UseProtoDirectoryWhenImportPackage ?
                $"{Config.Proto.ProtoDirectory}/{meta.FormatServiceName().ToSnakeString()}" :
                $"{meta.FormatServiceName().ToSnakeString()}";
        }

        public static string FormatServiceMethodName(this MethodMetaData method)
        {
            ArgumentNullException.ThrowIfNull(method, nameof(method));

            return Config?.Proto?.MethodNameFunc?.Invoke(method) ?? method.Name;
        }

        public static string FormatMessageName(this ProtocolMessage meta)
        {
            ArgumentNullException.ThrowIfNull(meta, nameof(meta));

            if (!meta.IsOriginalClass && meta.EnumMetaData == null)
                return meta.Name;

            return meta.Name.FormatMessageName();
        }

        public static string FormatMessageName(this string name)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));

            return Config?.Proto?.OriginalClassNameFunc?.Invoke(name) ?? name;
        }

        public static string ToProtobufString(this Type type, bool isNullable)
        {
            var result = Config.Proto.CSharpTypeToProtobufString?.Invoke(type, isNullable);
            if (!string.IsNullOrWhiteSpace(result))
                return result;

            return TypeConvert.Convert(type, isNullable);
        }

        public static string ToSnakeString(this string str)
        {
            var builder = new StringBuilder();
            var name = str;
            var previousUpper = false;

            for (var i = 0; i < name.Length; i++)
            {
                var c = name[i];
                if (char.IsUpper(c))
                {
                    if (i > 0 && !previousUpper)
                    {
                        builder.Append("_");
                    }
                    builder.Append(char.ToLowerInvariant(c));
                    previousUpper = true;
                }
                else
                {
                    builder.Append(c);
                    previousUpper = false;
                }
            }
            return builder.ToString();
        }

        public static string ToFirstLowString(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            return str.Substring(0, 1).ToLower() + str.Substring(1);
        }

        public static string ToFirstUpString(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }
    }
}

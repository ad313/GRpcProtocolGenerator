using System;
using System.Text;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Renders;

namespace GRpcProtocolGenerator
{
    public static class NameExtensions
    {
        public static GeneratorConfig config = null;

        public static string FormatServiceName(this InterfaceMetaData meta)
        {
            ArgumentNullException.ThrowIfNull(meta, nameof(meta));

            return config?.Proto?.ServiceNameFunc?.Invoke(meta) ?? meta.Name;
        }

        public static string FormatServiceProtoFileName(this InterfaceMetaData meta)
        {
            return meta.FormatServiceName().ToSnakeString();
        }

        public static string FormatServiceProtoFileNameFullPath(this InterfaceMetaData meta)
        {
            return config.Proto.UseProtoDirectoryWhenImportPackage ?
                $"{config.Proto.ProtoDirectory}/{meta.FormatServiceName().ToSnakeString()}" :
                $"{meta.FormatServiceName().ToSnakeString()}";
        }

        public static string FormatServiceMethodName(this MethodMetaData method)
        {
            ArgumentNullException.ThrowIfNull(method, nameof(method));

            return config?.Proto?.MethodNameFunc?.Invoke(method) ?? method.Name;
        }
        
        public static string FormatMessageName(this ProtoMessage meta)
        {
            ArgumentNullException.ThrowIfNull(meta, nameof(meta));

            if (!meta.IsOriginalClass && meta.EnumMetaData == null)
                return meta.Name;
            
            return meta.Name.FormatMessageName();
        }

        public static string FormatMessageName(this string name)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));

            return config?.Proto?.OriginalClassNameFunc?.Invoke(name) ?? name;
        }

        public static string ToProtobufString(this Type type, bool isNullable)
        {
            var result = config.Proto.CSharpTypeToProtobufString?.Invoke(type, isNullable);
            if (!string.IsNullOrWhiteSpace(result))
                return result;

            return ProtobufTypeConvert.Convert(type, isNullable);
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
                return String.Empty;

            return str.Substring(0, 1).ToLower() + str.Substring(1);
        }

        public static string ToFirstUpString(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return String.Empty;

            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }
    }
}

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace GRpcProtocolGenerator.Renders
{
    /// <summary>
    /// gRpc 字段类型转换
    /// </summary>
    public static class ProtobufTypeConvert
    {
        public static string Convert(Type type, bool isNullable)
        {
            switch (type.Name.ToLower())
            {
                case "object":
                case "guid":
                case "datetime":
                case "string":
                    return isNullable
                        ? CSharpNullableTypeEnum.String.GetDescription()
                        : CSharpTypeEnum.String.GetDescription();
                    break;
                case "decimal":
                case "double":
                    return isNullable
                        ? CSharpNullableTypeEnum.Double.GetDescription()
                        : CSharpTypeEnum.Double.GetDescription();
                case "float":
                case "single":
                    return isNullable
                        ? CSharpNullableTypeEnum.Float.GetDescription()
                        : CSharpTypeEnum.Float.GetDescription();
                case "int32":
                case "byte":
                    return isNullable
                        ? CSharpNullableTypeEnum.Int.GetDescription()
                        : CSharpTypeEnum.Int.GetDescription();
                case "long":
                case "int64":
                    return isNullable
                        ? CSharpNullableTypeEnum.Long.GetDescription()
                        : CSharpTypeEnum.Long.GetDescription();
                    break;
                case "uint32":
                    return isNullable
                        ? CSharpNullableTypeEnum.UInt.GetDescription()
                        : CSharpTypeEnum.UInt.GetDescription();
                case "ulong":
                case "uint64":
                    return isNullable
                        ? CSharpNullableTypeEnum.ULont.GetDescription()
                        : CSharpTypeEnum.ULong.GetDescription();
                    break;
                case "boolean":
                    return isNullable
                        ? CSharpNullableTypeEnum.Bool.GetDescription()
                        : CSharpTypeEnum.Bool.GetDescription();
                    break;
                case "byte[]":
                    return isNullable
                        ? CSharpNullableTypeEnum.ByteString.GetDescription()
                        : CSharpTypeEnum.ByteString.GetDescription();
                case "intptr":
                    return "int32";
                default:
                    throw new NotSupportedException(type.Name.ToLower());
            }
        }

        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DescriptionAttribute>()?
                .Description;
        }
    }


    public enum CSharpTypeEnum
    {
        /// <summary>
        /// Double 
        /// </summary>
        [Description("double")]
        Double = 0,
        /// <summary>
        /// Float 
        /// </summary>
        [Description("float")]
        Float = 1,
        /// <summary>
        /// Int 
        /// </summary>
        [Description("int32")]
        Int = 2,
        /// <summary>
        /// Long 
        /// </summary>
        [Description("int64")]
        Long = 3,
        /// <summary>
        /// UInt 
        /// </summary>
        [Description("uint32")]
        UInt = 4,
        /// <summary>
        /// ULont 
        /// </summary>
        [Description("uint64")]
        ULong = 5,
        /// <summary>
        /// Bool 
        /// </summary>
        [Description("bool")]
        Bool = 6,
        /// <summary>
        /// String 
        /// </summary>
        [Description("string")]
        String = 7,
        /// <summary>
        /// ByteString 
        /// </summary>
        [Description("bytes")]
        ByteString = 8,
    }

    public enum CSharpNullableTypeEnum
    {
        /// <summary>
        /// Double 
        /// </summary>
        [Description("google.protobuf.DoubleValue")]
        Double = 0,
        /// <summary>
        /// Float 
        /// </summary>
        [Description("google.protobuf.FloatValue")]
        Float = 1,
        /// <summary>
        /// Int 
        /// </summary>
        [Description("google.protobuf.Int32Value")]
        Int = 2,
        /// <summary>
        /// Long 
        /// </summary>
        [Description("google.protobuf.Int64Value")]
        Long = 3,
        /// <summary>
        /// UInt 
        /// </summary>
        [Description("google.protobuf.UInt32Value")]
        UInt = 4,
        /// <summary>
        /// ULont 
        /// </summary>
        [Description("google.protobuf.UInt64Value")]
        ULont = 5,
        /// <summary>
        /// Bool 
        /// </summary>
        [Description("google.protobuf.BoolValue")]
        Bool = 6,
        /// <summary>
        /// String 
        /// </summary>
        [Description("google.protobuf.StringValue")]
        String = 7,
        /// <summary>
        /// ByteString 
        /// </summary>
        [Description("google.protobuf.BytesValue")]
        ByteString = 8,
    }
}

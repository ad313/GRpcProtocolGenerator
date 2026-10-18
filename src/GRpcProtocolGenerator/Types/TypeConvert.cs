using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace GRpcProtocolGenerator.Types;

/// <summary>
/// gRpc 字段类型转换
/// </summary>
public static class TypeConvert
{
    public static string Convert(Type type, bool isNullable)
    {
        return type.Name.ToLower() switch
        {
            "object" or "guid" or "datetime" or "datetimeoffset" or "dateonly" or "timeonly" or "timespan" or "string" or "char" =>
                isNullable ? CSharpNullableTypeEnum.String.GetDescription() : CSharpTypeEnum.String.GetDescription(),
            "decimal" or "double" => isNullable ? CSharpNullableTypeEnum.Double.GetDescription() : CSharpTypeEnum.Double.GetDescription(),
            "float" or "single" or "half" => isNullable ? CSharpNullableTypeEnum.Float.GetDescription() : CSharpTypeEnum.Float.GetDescription(),
            "int32" or "int16" or "short" or "byte" or "sbyte" => isNullable ? CSharpNullableTypeEnum.Int.GetDescription() : CSharpTypeEnum.Int.GetDescription(),
            "long" or "int64" or "intptr" => isNullable ? CSharpNullableTypeEnum.Long.GetDescription() : CSharpTypeEnum.Long.GetDescription(),
            "uint32" or "uint16" or "ushort" => isNullable ? CSharpNullableTypeEnum.UInt.GetDescription() : CSharpTypeEnum.UInt.GetDescription(),
            "ulong" or "uint64" or "uintptr" => isNullable ? CSharpNullableTypeEnum.ULong.GetDescription() : CSharpTypeEnum.ULong.GetDescription(),
            "boolean" => isNullable ? CSharpNullableTypeEnum.Bool.GetDescription() : CSharpTypeEnum.Bool.GetDescription(),
            "byte[]" => isNullable ? CSharpNullableTypeEnum.ByteString.GetDescription() : CSharpTypeEnum.ByteString.GetDescription(),
            _ => throw new NotSupportedException(
                $"不支持的类型映射：C# 类型 '{type.FullName}' 无法转换为 protobuf 类型。" +
                "请通过 ProtocolConfig.CSharpTypeToProtobufString 自定义映射，或使用 [GRpcIgnore] 忽略该成员。"),
        };
    }

    /// <summary>
    /// 获取枚举的描述
    /// </summary>
    public static string GetDescription(this Enum value)
    {
        return value.GetType()
            .GetMember(value.ToString())
            .FirstOrDefault()?
            .GetCustomAttribute<DescriptionAttribute>()?
            .Description ?? value.ToString();
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
    /// ULong 
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
    /// ULong 
    /// </summary>
    [Description("google.protobuf.UInt64Value")]
    ULong = 5,
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

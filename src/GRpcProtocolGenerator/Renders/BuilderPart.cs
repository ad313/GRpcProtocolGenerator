using System;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Renders.Protocol;
using GRpcProtocolGenerator.Types;

namespace GRpcProtocolGenerator.Renders;

public partial class BuilderPart
{
    #region MapTo

    public static string MapTo(string name, string type)
    {
        return $"{name}.Adapt<{type}>()";
    }

    public static string MapToList(string name, string type)
    {
        return $"{name}.Adapt<List<{type}>>()";
    }

    public static string MapTo(bool isNullable, string name, string type)
    {
        return MapTo(isNullable ? $"{name}?" : name, type);
    }

    public static string MapToList(bool isNullable, string name, string type)
    {
        return MapToList(isNullable ? $"{name}?" : name, type);
    }

    /// <summary>
    /// 类、泛型类
    /// </summary>
    public static string MapTo(string name, Type typeWrapper)
    {
        if (!typeWrapper.IsGenericType)
        {
            return MapTo(true, name, typeWrapper.FullName!);
        }

        //泛型处理
        var type = typeWrapper.GetGenericTypeDefinition().FullName?.Replace($"`{typeWrapper.GetGenericArguments().Length}", "", StringComparison.Ordinal) + "<";
        foreach (var argument in typeWrapper.GetGenericArguments())
        {
            type += argument.FullName + ", ";
        }

        type = type.Trim().TrimEnd(',') + ">";

        return MapTo(true, name, type);
    }

    #endregion

    #region Method

    /// <summary>
    /// 方法定义的返回值
    /// </summary>
    public static string BuildMethodReturnType(bool isWrapper, bool isOutParamEmpty, string type, string emptyType)
    {
        if (isWrapper)
            return $"async Task<{type}>";

        return isOutParamEmpty
            ? $"async Task<{emptyType}>"
            : $"async Task<{type}>";
    }

    #endregion

    #region 输入

    /// <summary>
    /// 创建输入参数项
    /// </summary>
    public static string BuildInputItem(PropertyMetaData prop, string paramName)
    {
        return BuildInputItem(paramName, prop.Name!, prop.TypeWrapper.Type!, prop.TypeWrapper.IsArray, prop.TypeWrapper.IsNullable);
    }

    public static string BuildInputItem(string name, string paramName, Type type, bool isArray, bool isNullable)
    {
        var par = $"{name}.{paramName.ToFirstUpString()}";
        var typeWrapper = type.ToTypeWrapper();
        if (isArray)
        {
            if (typeWrapper.IsClass)
                return MapToList(true, par, typeWrapper.Type!.FullName!);

            if (typeWrapper.IsEnum)
                return MapToList(isNullable, par, typeWrapper.Type!.FullName!);

            return isNullable ? $"{par}?.ToList()" : $"{par}.ToList()";
        }

        if (typeWrapper.Type!.IsCancellationToken())
            return BuildCancellationTokenInput();

        if (typeWrapper.IsClass)
            return MapTo(par, typeWrapper.Type!);

        if (typeWrapper.IsEnum)
            return MapTo(isNullable, par, typeWrapper.Type!.FullName!);

        return par;
    }

    /// <summary>
    /// 创建 CancellationToken 输入参数
    /// </summary>
    public static string BuildCancellationTokenInput()
    {
        return "context.CancellationToken";
    }

    #endregion

    #region 输出

    /// <summary>
    /// 方法返回值 空
    /// </summary>
    public static string BuildEmptyReturn(string name, bool isWrapper, int successCode)
    {
        return isWrapper ? $"return new {name}" + " { Code = " + successCode + " };" : "return new Empty();";
    }

    /// <summary>
    /// 方法返回值 类
    /// </summary>
    public static string BuildClassReturn(string name, bool isArray, bool isWrapper, int successCode)
    {
        var body = isArray ? MapToList("data", name) : MapTo("data", name);
        return isWrapper ? $"return new {name}" + " { Code = " + successCode + ", Data = " + body + " };" : $"return {body};";
    }

    /// <summary>
    /// 返回值，构建类参数
    /// </summary>
    public static string BuildClassItemReturn(string name, bool isArray, string? gRpcType)
    {
        return isArray
            ? $"{name} = " + "{" + $" {MapToList("data", gRpcType!)} " + "}"
            : $"{name} = " + $"{MapTo("data", gRpcType!)}";
    }

    /// <summary>
    /// 返回值，构建普通参数（除枚举和类以外）
    /// </summary>
    public static string BuildSampleItemReturn(string name, bool isArray)
    {
        return isArray ? $"{name} = " + "{" + " data.ToList() " + "}" : $"{name} = data";
    }

    /// <summary>
    /// 返回值，构建枚举参数
    /// </summary>
    public static string BuildEnumItemReturn(string name, bool isArray, bool isNullable)
    {
        return isArray
            ? $"{name} = " + "{" + $" {MapToList("data", isNullable ? "int?" : "int")} " + "}"
            : $"{name} = " + (isNullable ? "(int?)data" : "(int)data");
    }

    #endregion

    #region 流式

    /// <summary>
    /// 获取流式元素的领域类型全名（用于 Adapt 映射与服务端实现签名）
    /// </summary>
    public static string GetStreamElementDomainName(PropertyMetaData element)
    {
        if (element.ClassMetaData != null)
            return element.ClassMetaData.FullName!;

        return element.TypeWrapper.Type!.FullName!;
    }

    /// <summary>
    /// 构建服务端流写入语句的右值：将领域元素映射为 proto 响应消息
    /// </summary>
    public static string BuildStreamResponseWrite(ProtoServiceItem item, string elementVarName)
    {
        var outParam = item.OutParam!;
        var protoName = outParam.GetGRpcName();

        //原始类：直接映射
        if (outParam.IsOriginalClass)
            return $"{elementVarName}.Adapt<{protoName}>()";

        //包装消息：唯一的 Data 字段赋值
        var field = outParam.Items.Last();
        var value = field.EnumMetaData != null
            ? $"({(field.IsNullable ? "int?" : "int")}){elementVarName}"
            : elementVarName;

        return $"new {protoName} {{ {field.Name} = {value} }}";
    }

    /// <summary>
    /// 构建客户端流读取映射：将 proto 请求消息映射为领域元素
    /// </summary>
    public static string BuildStreamRequestRead(ProtoServiceItem item, string itemVarName)
    {
        var inParam = item.InParam!;
        var element = item.MethodMetaData.StreamRequestElement!;

        //原始类：直接映射
        if (inParam.IsOriginalClass)
            return $"{itemVarName}.Adapt<{element.ClassMetaData!.FullName}>()";

        //包装消息：取唯一的 Data 字段
        var fieldName = inParam.Items.Last().Name!;
        if (element.EnumMetaData != null)
        {
            var enumTypeName = element.TypeWrapper.Type!.FullName!;
            var cast = element.TypeWrapper.IsNullable ? $"({enumTypeName}?)" : $"({enumTypeName})";
            return $"{cast}{itemVarName}.{fieldName}";
        }

        return $"{itemVarName}.{fieldName}";
    }

    #endregion

    #region proto message

    public static string? BuildMessageItem(string name, Type type, bool isArray, bool isNullable, int index, string? desc)
    {
        if (type.IsCancellationToken())
            return null;

        var wrapper = type.ToTypeWrapper();

        var typeString = wrapper.IsClass || wrapper.IsStruct
            ? wrapper.Type!.GetGenericClassName().FormatMessageName()
            : type.ToProtobufString(isNullable);

        return $"{(isArray ? "repeated " : "")}{typeString} {name} = {index};" + (string.IsNullOrWhiteSpace(desc) ? null : $" // {desc}");
    }

    #endregion
}

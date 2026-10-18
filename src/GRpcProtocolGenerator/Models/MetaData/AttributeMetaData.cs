using System;
using System.Collections.Generic;

namespace GRpcProtocolGenerator.Models.MetaData;

/// <summary>
/// Attribute 元数据
/// </summary>
public class AttributeMetaData(Type type, Dictionary<string, object?>? namedDictionary, Dictionary<int, object?>? corDictionary)
{
    public string Name { get; private set; } = type.Name;

    public string FullName { get; private set; } = type.FullName ?? type.Name;

    public Type Type { get; private set; } = type;

    /// <summary>
    /// 通过属性赋值的参数
    /// </summary>
    public Dictionary<string, object?>? NamedDictionary { get; private set; } = namedDictionary;

    /// <summary>
    /// 通过构造函数赋值的参数
    /// </summary>
    public Dictionary<int, object?>? ConstructorDictionary { get; private set; } = corDictionary;
}

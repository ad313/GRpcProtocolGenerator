using System.Collections.Generic;

namespace GRpcProtocolGenerator.Models.MetaData;

/// <summary>
/// 枚举元数据
/// </summary>
public class EnumMetaData(string? @namespace, string? name, List<AttributeMetaData> attributeMetaDataList, List<EnumMemberMetaData> members)
    : CommentMetaData(@namespace, name, attributeMetaDataList)
{
    /// <summary>
    /// 成员集合
    /// </summary>
    public List<EnumMemberMetaData> Members { get; private set; } = members;

    public InterfaceMetaData? InterfaceMetaData { get; private set; }

    public MethodMetaData? MethodMetaData { get; private set; }
}

/// <summary>
/// 枚举成员元数据
/// </summary>
public class EnumMemberMetaData(string name, int value, List<AttributeMetaData> attributeMetaDataList)
{
    /// <summary>
    /// 枚举名称
    /// </summary>
    public string Name { get; private set; } = name;

    /// <summary>
    /// 枚举值
    /// </summary>
    public int Value { get; private set; } = value;

    public List<AttributeMetaData> AttributeMetaDataList { get; private set; } = attributeMetaDataList;
}

using System.Collections.Generic;

namespace GRpcProtocolGenerator.Models.MetaData
{
    /// <summary>
    /// 枚举元数据
    /// </summary>
    public class EnumMetaData : CommentMetaData
    {
        /// <summary>
        /// 成员集合
        /// </summary>
        public List<EnumMemberMetaData> Members { get; private set; }

        public InterfaceMetaData InterfaceMetaData { get; private set; }

        public MethodMetaData MethodMetaData { get; private set; }

        public EnumMetaData(string @namespace, string name, List<AttributeMetaData> attributeMetaDataList, List<EnumMemberMetaData> members) : base(@namespace, name, attributeMetaDataList)
        {
            Members = members;
        }
    }

    /// <summary>
    /// 枚举成员元数据
    /// </summary>
    public class EnumMemberMetaData
    {
        /// <summary>
        /// 枚举名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 枚举值
        /// </summary>
        public int Value { get; private set; }

        public List<AttributeMetaData> AttributeMetaDataList { get; private set; }

        public EnumMemberMetaData(string name, int value, List<AttributeMetaData> attributeMetaDataList)
        {
            Name = name;
            Value = value;
            AttributeMetaDataList = attributeMetaDataList;
        }
    }
}
using System.Collections.Generic;
using GRpcProtocolGenerator.Types;

namespace GRpcProtocolGenerator.Models.MetaData
{
    /// <summary>
    /// 类 元数据
    /// </summary>
    public sealed class ClassMetaData : CommentMetaData
    {
        /// <summary>
        /// 属性集合
        /// </summary>
        public List<PropertyMetaData> PropertyMetaDataList { get; private set; }

        public TypeWrapper TypeWrapper { get; private set; }

        public ClassMetaData(TypeWrapper typeWrapper, string @namespace, string name, List<AttributeMetaData> attributeMetaDataList, List<PropertyMetaData> propertyMetaDataList)
            : base(@namespace, name, attributeMetaDataList)
        {
            TypeWrapper = typeWrapper;
            PropertyMetaDataList = propertyMetaDataList;

            if (typeWrapper.Type.IsGenericType)
            {
                name = typeWrapper.Type.GetGenericClassName();
                SetName(name);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using GRpcProtocolGenerator.Types;

namespace GRpcProtocolGenerator.Models.MetaData
{
    /// <summary>
    /// 属性元数据
    /// </summary>
    public class PropertyMetaData : CommentMetaData
    {
        public TypeWrapper TypeWrapper { get; }

        public ClassMetaData ClassMetaData { get; private set; }
        
        /// <summary>
        /// 枚举元数据
        /// </summary>
        public EnumMetaData EnumMetaData { get; private set; }

        public PropertyMetaData(TypeWrapper typeWrapper, string name, List<AttributeMetaData> attributeMetaDataList, ClassMetaData classMetaData, EnumMetaData enumMetaData)
            : base(typeWrapper.Type.Name, name, attributeMetaDataList)
        {
            TypeWrapper = typeWrapper;
            ClassMetaData = classMetaData;
            EnumMetaData = enumMetaData;

            //根据attribute设置是否可为空，require 和 nullable
            typeWrapper.SetNullable(attributeMetaDataList?.Select(d => d.Type).ToList());
        }

        public void SetClassMetaData(ClassMetaData classMetaData)
        {
            ClassMetaData = classMetaData;
        }
    }
}
using System;
using System.Collections.Generic;

namespace GRpcProtocolGenerator.Models.MetaData
{
    /// <summary>
    /// Attribute 元数据
    /// </summary>
    public class AttributeMetaData
    {
        public string Name { get; private set; }

        public string FullName { get; private set; }

        public Type Type { get; private set; }

        /// <summary>
        /// 通过属性赋值的参数
        /// </summary>
        public Dictionary<string, object> NamedDictionary { get; private set; }

        /// <summary>
        /// 通过构造函数赋值的参数
        /// </summary>
        public Dictionary<int, object> ConstructorDictionary { get; private set; }

        public AttributeMetaData(Type type, Dictionary<string, object> namedDictionary, Dictionary<int, object> corDictionary)
        {
            Name = type.Name;
            FullName = type.FullName;
            Type = type;
            NamedDictionary = namedDictionary;
            ConstructorDictionary = corDictionary;
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GRpcProtocolGenerator.Models.MetaData
{
    /// <summary>
    /// 注释元数据
    /// </summary>
    public class CommentMetaData
    {
        /// <summary>
        /// 从 Display Attribute 提取的注释
        /// </summary>
        public string Display { get; private set; }

        /// <summary>
        /// 从 DisplayName Attribute 提取的注释
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// 从 Description Attribute 提取的注释
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 方法的原始注释
        /// </summary>
        public string OriginalComment { get; private set; }


        public string Namespace { get; private set; }

        public string Name { get; private set; }
        
        public string FullName => $"{Namespace}.{Name}";

        /// <summary>
        /// Attribute
        /// </summary>
        public List<AttributeMetaData> AttributeMetaDataList { get; private set; }
        
        public CommentMetaData(string @namespace, string name, List<AttributeMetaData> attributeMetaDataList)
        {
            Namespace = @namespace;
            Name = name;
            AttributeMetaDataList = attributeMetaDataList;

            Display = AttributeMetaDataList.FirstOrDefault(d => d.Type == typeof(DisplayAttribute))?.NamedDictionary.FirstOrDefault(d => d.Key == "Name").Value?.ToString();
            DisplayName = AttributeMetaDataList.FirstOrDefault(d => d.Type == typeof(DisplayNameAttribute))?.ConstructorDictionary.FirstOrDefault(d => d.Key == 0).Value?.ToString();
            Description = AttributeMetaDataList.FirstOrDefault(d => d.Type == typeof(DescriptionAttribute))?.ConstructorDictionary.FirstOrDefault(d => d.Key == 0).Value?.ToString();
        }

        public virtual void SetName(string name)
        {
            Name = name;
        }
    }
}
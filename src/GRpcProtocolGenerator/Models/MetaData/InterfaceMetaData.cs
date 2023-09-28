using System;
using System.Collections.Generic;
using System.Linq;

namespace GRpcProtocolGenerator.Models.MetaData
{
    public class InterfaceMetaData : CommentMetaData
    {
        public Type Type { get; private set; }

        public string Key { get; private set; }

        public InterfaceMetaData Parent { get; private set; }

        public List<InterfaceMetaData> Children { get; private set; }

        public List<MethodMetaData> MethodMetaDataList { get; private set; }

        /// <summary>
        /// 如果接口是泛型，泛型参数对象
        /// </summary>
        public List<PropertyMetaData> ParamMetaDataList { get; private set; }

        public bool Enable { get; private set; }

        public InterfaceMetaData(string @namespace, string name, Type type, List<AttributeMetaData> attributeMetaDataList, List<PropertyMetaData> paramMetaDataList, List<MethodMetaData> methodMetaDataList)
            : base(@namespace, name, attributeMetaDataList)
        {
            Type = type;
            Enable = true;
            ParamMetaDataList = paramMetaDataList;
            MethodMetaDataList = methodMetaDataList;
            Children = new List<InterfaceMetaData>();

            for (var i = 0; i < MethodMetaDataList.Count; i++)
            {
                MethodMetaDataList[i].Index = i;
            }
        }

        public void SetKey()
        {
            Key = !ParamMetaDataList.Any() ? FullName : $"{FullName}_{string.Join("_", ParamMetaDataList.Select(d => d.FullName))}";
        }

        public void SetMethodInterface()
        {
            MethodMetaDataList.ForEach(item => item.SetInterfaceMetaData(this));
        }

        public bool Exists(string key)
        {
            return key == Key || Children.Any(d => d.Exists(key));
        }

        public void SetEnable(bool enable)
        {
            Enable = enable;
        }

        public void SetParent(InterfaceMetaData parent)
        {
            Parent = parent;
        }

        public void SetChildren(List<InterfaceMetaData> children)
        {
            Children = children;
        }
    }
}
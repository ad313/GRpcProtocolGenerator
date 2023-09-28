using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GRpcProtocolGenerator.Models.MetaData
{
    public class AssemblyMetaData
    {
        public string Name { get; private set; }

        public string FullName { get; private set; }

        /// <summary>
        /// Interface 元数据
        /// </summary>
        public Dictionary<string, InterfaceMetaData> InterfaceMetaDataDictionary { get; private set; }

        /// <summary>
        /// 类元数据
        /// </summary>
        public Dictionary<string, ClassMetaData> ClassMetaDataDictionary { get; private set; }

        /// <summary>
        /// enum 元数据
        /// </summary>
        public Dictionary<string, EnumMetaData> EnumMetaDataDictionary { get; private set; }

        /// <summary>
        /// 不支持的方法
        /// </summary>
        public List<MethodInfo> NotSupportMethodList { get; private set; }

        public AssemblyMetaData(string name, string fullName,
            Dictionary<string, InterfaceMetaData> interfaceMetaDataDictionary,
            Dictionary<string, ClassMetaData> classMetaDataDictionary,
            Dictionary<string, EnumMetaData> enumMetaDataDictionary,
            List<MethodInfo> notSupportMethodList)
        {
            Name = name;
            FullName = fullName;
            InterfaceMetaDataDictionary = interfaceMetaDataDictionary;
            ClassMetaDataDictionary = classMetaDataDictionary;
            EnumMetaDataDictionary = enumMetaDataDictionary;
            NotSupportMethodList = notSupportMethodList;

            ClearNotSupport();
        }

        /// <summary>
        /// 过滤不支持的方法和属性
        /// </summary>
        private void ClearNotSupport()
        {
            foreach (var pair in InterfaceMetaDataDictionary)
            {
                var newList = pair.Value.MethodMetaDataList
                    .Where(d => d.MethodInfo.CustomAttributes.IsGRpcIgnore() == false)
                    .Where(d => d.InParamMetaDataList.All(p => p.TypeWrapper.IsSupport()) && d.OutParamMetaDataList.All(p => p.TypeWrapper.IsSupport()))
                    .ToList();

                if (newList.Count != pair.Value.MethodMetaDataList.Count)
                {
                    pair.Value.MethodMetaDataList.Clear();
                    pair.Value.MethodMetaDataList.AddRange(newList);
                }
            }
        }

        /// <summary>
        /// 数据过滤
        /// </summary>
        /// <param name="config"></param>
        public AssemblyMetaData Filter(GeneratorConfig config)
        {
            if (config?.Filter == null)
                return this;

            if (config.Filter.InterfaceFilterFunc != null)
            {
                InterfaceMetaDataDictionary = InterfaceMetaDataDictionary.Select(d => d.Value)
                    .Where(d => config.Filter.InterfaceFilterFunc(d)).ToDictionary(d => d.FullName, d => d);
            }

            if (config.Filter.MethodFilterFunc != null)
            {
                foreach (var pair in InterfaceMetaDataDictionary)
                {
                    var newList = pair.Value.MethodMetaDataList.Where(d => config.Filter.MethodFilterFunc(pair.Value, d)).ToList();
                    if (newList.Count != pair.Value.MethodMetaDataList.Count)
                    {
                        pair.Value.MethodMetaDataList.Clear();
                        pair.Value.MethodMetaDataList.AddRange(newList);
                    }
                }
            }

            SetMethodInterface();

            return this;
        }

        private AssemblyMetaData SetMethodInterface()
        {
            foreach (var pair in InterfaceMetaDataDictionary)
            {
                pair.Value.SetMethodInterface();
            }

            return this;
        }
    }
}